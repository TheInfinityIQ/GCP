using System.Collections.Concurrent;
using System.Text.Json;

using Gameloop.Vdf;
using Gameloop.Vdf.Linq;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GCP.Api.Controllers;

[AllowAnonymous]
public class SteamController : ApiController
{
	private static readonly ConcurrentDictionary<long, string> _steamGameNames = new();
	private readonly ILogger<SteamController> _logger;
	private readonly HttpClient _httpClient;

	public SteamController(ILogger<SteamController> logger, HttpClient httpClient)
	{
		_logger = logger;
		_httpClient = httpClient;
	}

	[HttpPost("parse-vdf")]
	public async Task<ActionResult> ParseVdf(IFormFile vdf, CancellationToken cancellationToken = default)
	{
		try
		{
			await UpdateSteamGameCache(cancellationToken);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "failed to retrieve steam games");
			AddEmptyModelError("[INTERNAL] failed to retrieve steam games");
			return StatusCode(500, ModelState);
		}

		using var stream = vdf.OpenReadStream();
		using var sr = new StreamReader(stream);

		if (stream is { CanRead: false } or { Length: 0 } || sr.EndOfStream)
		{
			AddEmptyModelError("VDF file cannot be empty.");
			return BadRequest(ModelState);
		}

		VProperty root;
		try
		{
			root = VdfConvert.Deserialize(sr);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "error when parsing VDF");
			AddEmptyModelError("failed to parse VDF file.");
			return BadRequest(ModelState);
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
			.OrderByDescending(x => x.Key)
			.ToDictionary(x => x.Key, x => x.Value);

		return Ok(games);
	}

	private async Task UpdateSteamGameCache(CancellationToken cancellationToken)
	{
		if (_steamGameNames.IsEmpty)
		{
			_logger.LogInformation("[START] Updating steam game cache.");

			var steamAppListUri = new Uri("https://api.steampowered.com/ISteamApps/GetAppList/v2/");
			var getSteamAppResponse = await _httpClient.GetAsync(steamAppListUri, cancellationToken);
			var steamAppJson = await getSteamAppResponse.Content.ReadAsStringAsync(cancellationToken);

			var jDoc = JsonDocument.Parse(steamAppJson);
			var stuff = jDoc.RootElement
				.GetProperty("applist")
				.GetProperty("apps");

			var appNames = stuff
				.EnumerateArray()
				.Select(x => (AppId: x.GetProperty("appid").GetInt64(), Name: x.GetProperty("name").GetString()))
				.Where(x => x.AppId is > 0 && !string.IsNullOrWhiteSpace(x.Name))
				.GroupBy(x => x.AppId, (k, r) => r.Where(x => !string.IsNullOrWhiteSpace(x.Name)).FirstOrDefault())
				.ToDictionary(x => x.AppId, x => x.Name!);

			foreach (var (appId, appName) in appNames)
			{
				if (_steamGameNames.ContainsKey(appId))
				{
					_steamGameNames[appId] = appName;
					continue;
				}

				_steamGameNames.TryAdd(appId, appName);
			}

			_logger.LogInformation("[FINISH] Updated steam game cache.");
		}
	}
}

