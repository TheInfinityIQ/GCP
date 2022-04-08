using System.Collections.Concurrent;
using System.Text.Json;

using Gameloop.Vdf;
using Gameloop.Vdf.Linq;

using GCP.Api.Controllers;
using GCP.Api.Data;
using GCP.Api.Data.Entities;
using GCP.Api.DTOs;
using GCP.Api.Utilities;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GCP.Api.Services;

public interface ISteamSerivce
{
	SteamSerivce AddOrUpdateSteamAppNameCache(IDictionary<long, string> steamAppNameCache);
	Task<GCPResult<ParseVdfResponseDTO>> ParseVdfAsync(ParseVdfRequestDTO requestDTO, CancellationToken cancellationToken = default);
}

public class SteamSerivce : ISteamSerivce
{
	private readonly ConcurrentDictionary<long, string> _steamGameNames = new();
	private readonly ILogger<SteamController> _logger;
	private readonly IConfiguration _configuration;
	private readonly IMemoryCache _memoryCache;
	private readonly HttpClient _httpClient;
	private readonly GCPContext _context;

	public SteamSerivce(ILogger<SteamController> logger, IConfiguration configuration, IMemoryCache memoryCache, HttpClient httpClient, GCPContext context)
	{
		_logger = logger;
		_httpClient = httpClient;
		_configuration = configuration;
		_memoryCache = memoryCache;
		_context = context;
	}

	public static IDictionary<long, string> ParseSteamAppNames(string appListJson)
	{
		var jDoc = JsonDocument.Parse(appListJson);
		var steamApps = jDoc.RootElement
			.GetProperty("applist")
			.GetProperty("apps");

		var steamAppNames = steamApps
			.EnumerateArray()
			.Select(x => (AppId: x.GetProperty("appid").GetInt64(), Name: x.GetProperty("name").GetString()))
			.Where(x => x.AppId is > 0 && !string.IsNullOrWhiteSpace(x.Name))
			.GroupBy(x => x.AppId, (k, r) => r.Where(x => !string.IsNullOrWhiteSpace(x.Name)).FirstOrDefault())
			.OrderBy(x => x.AppId)
			.ToDictionary(x => x.AppId, x => x.Name!);

		return steamAppNames;
	}

	public SteamSerivce AddOrUpdateSteamAppNameCache(IDictionary<long, string> steamAppNameCache)
	{
		foreach (var (appId, appName) in steamAppNameCache)
		{
			if (_steamGameNames.ContainsKey(appId))
			{
				_steamGameNames[appId] = appName;
				continue;
			}

			_steamGameNames.TryAdd(appId, appName);
		}
		return this;
	}

	public async Task<GCPResult<ParseVdfResponseDTO>> ParseVdfAsync(ParseVdfRequestDTO requestDTO, CancellationToken cancellationToken = default)
	{
		var (currentUserId, vdfStream) = requestDTO;

		var stream = vdfStream;
		var sr = new StreamReader(stream);

		if (stream is { CanRead: false } or { Length: 0 } || sr.EndOfStream)
		{
			const string logMessage = "VDF file cannot be empty.";
			var errorCode = GCPErrorCode.EmptyVdfFile;
			_logger.LogError(errorCode.ToEventId(), logMessage);
			return GCPResult.Failure<ParseVdfResponseDTO>(errorCode, logMessage);
		}

		VProperty root;
		try
		{
			root = VdfConvert.Deserialize(sr);
		}
		catch (Exception ex)
		{
			const string logMessage = "error when parsing VDF";
			var errorCode = GCPErrorCode.FailedToParseGameLibraryVdfFile;
			_logger.LogError(errorCode.ToEventId(), ex, logMessage);
			return GCPResult.Failure<ParseVdfResponseDTO>(errorCode, logMessage);
		}

		var vdfApps = root.Value.OfType<VProperty>()
			.Where(x => int.TryParse(x.Key, out _))
			.Select(x => x.Value)
			.OfType<VObject>()
			.Select(x => x.TryGetValue("apps", out var apps) ? apps : null)
			.Where(x => x is not null)
			.SelectMany(x => x!.OfType<VProperty>())
			.Select(x => (Key: long.Parse(x.Key), Value: x.Value.Value<long>()))
			.Select(x => x.Key)
			.ToHashSet();

		var games = _steamGameNames
			.Where(x => vdfApps.Contains(x.Key))
			.OrderBy(x => x.Key)
			.Select(x => new ParsedSteamAppNameDTO(x.Key, x.Value))
			.ToHashSet();

		if (currentUserId is not null)
		{
			var userId = currentUserId.Value;
			var user = await _context.Users.Include(x => x.OwnedGames).SingleAsync(x => x.Id == userId, cancellationToken);
			var existingOwnedSteamAppIds = user.OwnedGames.Select(x => x.SteamAppId).ToHashSet();

			var allGames = await _context.Game.Include(x => x.Owners.Where(u => u.Id == userId))
				.Where(x => x.SteamAppId.HasValue)
				.ToDictionaryAsync(x => x.SteamAppId!.Value, x => x, cancellationToken);

			foreach (var (appId, name) in games)
			{
				if (!allGames.ContainsKey(appId))
				{
					var game = new Game
					{
						Name = name,
						NormalizedName = name.ToUpperInvariant(),
						SteamAppId = appId,
					};

					await _context.Game.AddAsync(game, cancellationToken);
					user.OwnedGames.Add(game);
				}
				else if (!existingOwnedSteamAppIds.Contains(appId))
				{
					user.OwnedGames.Add(allGames[appId]);
				}
			}

			await _context.SaveChangesAsync(cancellationToken);
		}

		return GCPResult.Success<ParseVdfResponseDTO>(new(games));
	}

	public async Task<GCPResult<IDictionary<long, string>>> GetSteamAppListAsync(CancellationToken cancellationToken)
	{
		if (_memoryCache.TryGetValue<IDictionary<long, string>>(GCPConst.CacheKey.SteamAppNames, out var steamAppNames) && steamAppNames is not null)
		{
			return GCPResult.Success(steamAppNames);
		}

		var steamAppListUri = _configuration.TryGetSteamApiKey(out var steamApiKey)
			? new Uri($"https://api.steampowered.com/ISteamApps/GetAppList/v2/?key={steamApiKey}")
			: new Uri("https://api.steampowered.com/ISteamApps/GetAppList/v2/");
		var getSteamAppResponse = await _httpClient.GetAsync(steamAppListUri, cancellationToken);
		var steamAppJson = await getSteamAppResponse.Content.ReadAsStringAsync(cancellationToken);

		var result = ParseSteamAppNames(steamAppJson);
		_memoryCache.Set(GCPConst.CacheKey.SteamAppNames, result);
		return GCPResult.Success(result);
	}
}
