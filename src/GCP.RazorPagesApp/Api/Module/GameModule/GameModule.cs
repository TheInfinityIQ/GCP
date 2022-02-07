
using GCP.RazorPagesApp.Data;
using GCP.RazorPagesApp.Data.Entities;

using Microsoft.EntityFrameworkCore;

namespace GCP.RazorPagesApp.Api.Module.GameModule;
public class GameModule : BaseModule
{
	public string BaseUrl => $"{RoutePrefix}/game";

	public override void AddServices(IServiceCollection services)
	{
	}

	public override void MapEndpoints(IEndpointRouteBuilder app)
	{
		var getGameList = app.MapGet(BaseUrl, async (GCPContext context, CancellationToken cancellationToken) =>
		{
			var games = await context.Game.AsNoTracking()
				.Include(g => g.Endorsements)
					.ThenInclude(e => e.User)
				.Include(g => g.Publishers)
				.Include(g => g.Retailers)
				.Include(g => g.GamePublisherPlatforms)
					.ThenInclude(gpp => gpp.Platform)
				.Include(g => g.GamePublishers)
					.ThenInclude(gp => gp.Platforms)
				.Include(g => g.GamePublishers)
					.ThenInclude(gp => gp.Publisher)
				.Include(g => g.GameLinks)
				.Include(g => g.Genres)
				.Select(g => new GameListItemDTO(g))
				.ToListAsync(cancellationToken);
			return Results.Ok(games);
		});
		getGameList
			.AllowAnonymous()
			.WithTags(nameof(GameModule))
			.Produces<GameListItemDTO[]>(200);

		var getGameById = app.MapGet($"{BaseUrl}/{{id:int}}", async (int id, GCPContext context, CancellationToken cancellationToken) =>
		{
			var game = await context.Game.AsNoTracking()
				.Include(g => g.Endorsements)
					.ThenInclude(e => e.User)
				.Include(g => g.Publishers)
				.Include(g => g.Retailers)
				.Include(g => g.GamePublisherPlatforms)
					.ThenInclude(gpp => gpp.Platform)
				.Include(g => g.GamePublishers)
					.ThenInclude(gp => gp.Platforms)
				.Include(g => g.GamePublishers)
					.ThenInclude(gp => gp.Publisher)
				.Include(g => g.GameLinks)
				.Include(g => g.Genres)
				.Where(x => x.Id == id)
				.Select(g => new GameListItemDTO(g))
				.SingleOrDefaultAsync(cancellationToken);
			if (game is null)
			{
				return Results.NotFound();
			}
			return Results.Ok(game);
		});
		getGameById
			.AllowAnonymous()
			.WithTags(nameof(GameModule))
			.Produces<GameListItemDTO>(200)
			.Produces(404);

		var postGame = app.MapPost(BaseUrl, async (CreateGameDTO inputDTO, GCPContext context, CancellationToken cancellationToken) =>
		{
			var newGame = new Game();
			var errors = new Dictionary<string, string[]>();

			foreach (var link in inputDTO.Links ?? Array.Empty<LinkDTO>())
			{
				//if (!Uri.TryCreate(link.Url, UriKind.Absolute, out _))
				if (string.IsNullOrWhiteSpace(link.Url?.OriginalString))
				{
					errors.Add(nameof(link), new[] { "must be a valid absolute link." });
					continue;
				}

				newGame.GameLinks.Add(new() { Link = link.Url, Label = link.Label, Type = link.Type });
			}

			if (inputDTO.Endorsement is not null)
			{
				var users = await context.Users.ToListAsync(cancellationToken);
				if (inputDTO.Endorsement.Weight is < 0)
				{
					errors.Add(nameof(inputDTO.Endorsement.Weight), new[] { "can't be less than zero." });
				}

				var user = users.SingleOrDefault(u => u.Id == inputDTO.Endorsement.UserId);
				if (user is null)
				{
					errors.Add(nameof(inputDTO.Endorsement.UserId), new[] { "user not found." });
				}
				else
				{
					newGame.Endorsements.Add(new() { Weight = inputDTO.Endorsement.Weight, UserId = user.Id });
				}
			}



			var publishers = await context.Publisher.ToListAsync(cancellationToken);
			var platforms = await context.Platform.ToListAsync(cancellationToken);
			foreach (var publisherDTO in inputDTO.Publishers ?? Array.Empty<PublisherDTO>())
			{
				var publisher = publishers.SingleOrDefault(p => p.Id == publisherDTO.Id);
				if (publisherDTO.Id is 0)
				{
					if (string.IsNullOrWhiteSpace(publisherDTO.Name))
					{
						errors.Add(nameof(inputDTO.Publishers), new[] { "publisher name cannot be null or whitespace." });
						continue;
					}
					publisher = new()
					{
						Name = publisherDTO.Name,
						NormalizedName = publisherDTO.Name.ToUpperInvariant(),
					};

					if (publishers.Any(p => p.NormalizedName == publisher.NormalizedName))
					{
						errors.Add(nameof(inputDTO.Publishers), new[] { "publisher name is already taken." });
						continue;
					}
				}
				else if (publisher is null)
				{
					errors.Add(nameof(inputDTO.Publishers), new[] { $"publisher '{publisherDTO.Id}' not found." });
					continue;
				}

				var publisherPlatforms = new List<Platform>();
				foreach (var platformDTO in publisherDTO.Platforms ?? Array.Empty<NameRecordDTO>())
				{
					var platform = platforms.SingleOrDefault(p => p.Id == platformDTO.Id);
					if (platformDTO.Id is 0)
					{
						if (string.IsNullOrWhiteSpace(platformDTO.Name))
						{
							errors.Add(nameof(inputDTO.Publishers), new[] { "platform name cannot be null or whitespace." });
							continue;
						}
						platform = new()
						{
							Name = platformDTO.Name,
							NormalizedName = platformDTO.Name.ToUpperInvariant(),
						};

						if (platforms.Any(p => p.NormalizedName == platform.NormalizedName))
						{
							errors.Add(nameof(inputDTO.Publishers), new[] { "platform name is already taken." });
							continue;
						}
					}
					else if (platform is null)
					{
						errors.Add(nameof(inputDTO.Publishers), new[] { $"publisher's '{publisher.Id}' platform '{platformDTO.Id}' not found." });
						continue;
					}
					publisherPlatforms.Add(platform);
				}

				newGame.GamePublishers.Add(new()
				{
					Publisher = publisher,
					Platforms = publisherPlatforms,
				});
			}


			var genres = await context.Genre.ToListAsync(cancellationToken);
			foreach (var genreDTO in inputDTO.Genres ?? Array.Empty<NameRecordDTO>())
			{
				var genre = genres.SingleOrDefault(g => g.Id == genreDTO.Id);
				if (genreDTO.Id is 0)
				{
					if (string.IsNullOrWhiteSpace(genreDTO.Name))
					{
						errors.Add(nameof(inputDTO.Genres), new[] { "genre name cannot be null or whitespace." });
						continue;
					}
					genre = new()
					{
						Name = genreDTO.Name,
						NormalizedName = genreDTO.Name.ToUpperInvariant(),
					};

					if (genres.Any(p => p.NormalizedName == genre.NormalizedName))
					{
						errors.Add(nameof(inputDTO.Genres), new[] { "genre name is already taken." });
						continue;
					}
				}
				else if (genre is null)
				{
					errors.Add(nameof(inputDTO.Genres), new[] { $"genre '{genreDTO.Id}' not found." });
					continue;
				}
				newGame.Genres.Add(genre);
			}

			var retailers = await context.Retailer.ToListAsync(cancellationToken);
			foreach (var retailerDTO in inputDTO.Retailers ?? Array.Empty<NameRecordDTO>())
			{
				var retailer = retailers.SingleOrDefault(g => g.Id == retailerDTO.Id);
				if (retailerDTO.Id is 0)
				{
					if (string.IsNullOrWhiteSpace(retailerDTO.Name))
					{
						errors.Add(nameof(inputDTO.Retailers), new[] { "retailer name cannot be null or whitespace." });
						continue;
					}
					retailer = new()
					{
						Name = retailerDTO.Name,
						NormalizedName = retailerDTO.Name.ToUpperInvariant(),
					};

					if (retailers.Any(p => p.NormalizedName == retailer.NormalizedName))
					{
						errors.Add(nameof(inputDTO.Retailers), new[] { "retailer name is already taken." });
						continue;
					}
				}
				else if (retailer is null)
				{
					errors.Add(nameof(inputDTO.Publishers), new[] { $"retailer '{retailerDTO.Id}' not found." });
					continue;
				}
				newGame.Retailers.Add(retailer);
			}


			if (string.IsNullOrWhiteSpace(inputDTO.Title))
			{
				errors.Add(nameof(inputDTO.Title), new[] { "title cannot be null or whitespace." });
			}
			else
			{
				newGame.Title = inputDTO.Title;
				newGame.NormalizedTitle = inputDTO.Title.ToUpperInvariant();
			}

			if (await context.Game.AnyAsync(g => g.NormalizedTitle == newGame.NormalizedTitle, cancellationToken) is true)
			{
				errors.Add(nameof(inputDTO.Title), new[] { "title is already taken." });
			}

			newGame.ReleaseDate = inputDTO.ReleaseDate;
			newGame.SupportsOnlinePlay = inputDTO.SupportsOnlinePlay;
			newGame.MaxPlayers = inputDTO.MaxPlayers;

			if (errors.Any())
			{
				return Results.BadRequest(new HttpValidationProblemDetails(errors));
			}

			await context.Game.AddAsync(newGame, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);

			var dto = new GameListItemDTO(newGame);

			return Results.Created(BaseUrl + $"/{newGame.Id}", dto);
		});
		postGame
			.AllowAnonymous()
			.WithTags(nameof(GameModule))
			.Accepts<CreateGameDTO>(isOptional: false, "application/json")
			.Produces<GameListItemDTO>(200)
			.Produces<HttpValidationProblemDetails>(400)
			.Produces(404);

		var putGame = app.MapPut($"{BaseUrl}/{{id:int}}", async (int id, UpdateGameDTO inputDTO, GCPContext context, CancellationToken cancellationToken) =>
		{
			var errors = new Dictionary<string, string[]>();
			var newGame = await context.Game.SingleOrDefaultAsync(g => g.Id == id, cancellationToken);
			if (newGame is null)
			{
				errors.Add(nameof(id), new[] { $"game not found by id '{id}'" });
				return Results.NotFound(new HttpValidationProblemDetails(errors));
			}
			newGame.Genres.Clear();
			newGame.Retailers.Clear();
			newGame.Publishers.Clear();
			newGame.Endorsements.Clear();
			newGame.GameLinks.Clear();
			newGame.GameGenres.Clear();
			newGame.GameRetailers.Clear();
			newGame.GamePublishers.Clear();
			newGame.GamePublisherPlatforms.Clear();

			foreach (var link in inputDTO.Links ?? Array.Empty<LinkDTO>())
			{
				//if (!Uri.TryCreate(link.Url, UriKind.Absolute, out _))
				if (string.IsNullOrWhiteSpace(link.Url?.OriginalString))
				{
					errors.Add(nameof(link), new[] { "must be a valid absolute link." });
					continue;
				}

				newGame.GameLinks.Add(new() { Link = link.Url, Label = link.Label, Type = link.Type });
			}

			var users = await context.Users.ToListAsync(cancellationToken);
			foreach (var endorsement in inputDTO.Endorsements ?? Array.Empty<EndorsementDTO>())
			{
				if (endorsement.Weight is < 0)
				{
					errors.Add(nameof(endorsement.Weight), new[] { "can't be less than zero." });
				}

				var user = users.SingleOrDefault(u => u.Id == endorsement.UserId);
				if (user is null)
				{
					errors.Add(nameof(endorsement.UserId), new[] { "user not found." });
				}
				else
				{
					newGame.Endorsements.Add(new() { Weight = endorsement.Weight, UserId = user.Id });
				}
			}


			var publishers = await context.Publisher.ToListAsync(cancellationToken);
			var platforms = await context.Platform.ToListAsync(cancellationToken);
			foreach (var publisherDTO in inputDTO.Publishers ?? Array.Empty<PublisherDTO>())
			{
				var publisher = publishers.SingleOrDefault(p => p.Id == publisherDTO.Id);
				if (publisherDTO.Id is 0)
				{
					if (string.IsNullOrWhiteSpace(publisherDTO.Name))
					{
						errors.Add(nameof(inputDTO.Publishers), new[] { "publisher name cannot be null or whitespace." });
						continue;
					}
					publisher = new()
					{
						Name = publisherDTO.Name,
						NormalizedName = publisherDTO.Name.ToUpperInvariant(),
					};

					if (publishers.Any(p => p.NormalizedName == publisher.NormalizedName))
					{
						errors.Add(nameof(inputDTO.Publishers), new[] { "publisher name is already taken." });
						continue;
					}
				}
				else if (publisher is null)
				{
					errors.Add(nameof(inputDTO.Publishers), new[] { $"publisher '{publisherDTO.Id}' not found." });
					continue;
				}

				var publisherPlatforms = new List<Platform>();
				foreach (var platformDTO in publisherDTO.Platforms ?? Array.Empty<NameRecordDTO>())
				{
					var platform = platforms.SingleOrDefault(p => p.Id == platformDTO.Id);
					if (platformDTO.Id is 0)
					{
						if (string.IsNullOrWhiteSpace(platformDTO.Name))
						{
							errors.Add(nameof(inputDTO.Publishers), new[] { "platform name cannot be null or whitespace." });
							continue;
						}
						platform = new()
						{
							Name = platformDTO.Name,
							NormalizedName = platformDTO.Name.ToUpperInvariant(),
						};

						if (platforms.Any(p => p.NormalizedName == platform.NormalizedName))
						{
							errors.Add(nameof(inputDTO.Publishers), new[] { "platform name is already taken." });
							continue;
						}
					}
					else if (platform is null)
					{
						errors.Add(nameof(inputDTO.Publishers), new[] { $"publisher's '{publisher.Id}' platform '{platformDTO.Id}' not found." });
						continue;
					}
					publisherPlatforms.Add(platform);
				}

				newGame.GamePublishers.Add(new()
				{
					Publisher = publisher,
					Platforms = publisherPlatforms,
				});
			}


			var genres = await context.Genre.ToListAsync(cancellationToken);
			foreach (var genreDTO in inputDTO.Genres ?? Array.Empty<NameRecordDTO>())
			{
				var genre = genres.SingleOrDefault(g => g.Id == genreDTO.Id);
				if (genreDTO.Id is 0)
				{
					if (string.IsNullOrWhiteSpace(genreDTO.Name))
					{
						errors.Add(nameof(inputDTO.Genres), new[] { "genre name cannot be null or whitespace." });
						continue;
					}
					genre = new()
					{
						Name = genreDTO.Name,
						NormalizedName = genreDTO.Name.ToUpperInvariant(),
					};

					if (genres.Any(p => p.NormalizedName == genre.NormalizedName))
					{
						errors.Add(nameof(inputDTO.Genres), new[] { "genre name is already taken." });
						continue;
					}
				}
				else if (genre is null)
				{
					errors.Add(nameof(inputDTO.Genres), new[] { $"genre '{genreDTO.Id}' not found." });
					continue;
				}
				newGame.Genres.Add(genre);
			}

			var retailers = await context.Retailer.ToListAsync(cancellationToken);
			foreach (var retailerDTO in inputDTO.Retailers ?? Array.Empty<NameRecordDTO>())
			{
				var retailer = retailers.SingleOrDefault(g => g.Id == retailerDTO.Id);
				if (retailerDTO.Id is 0)
				{
					if (string.IsNullOrWhiteSpace(retailerDTO.Name))
					{
						errors.Add(nameof(inputDTO.Retailers), new[] { "retailer name cannot be null or whitespace." });
						continue;
					}
					retailer = new()
					{
						Name = retailerDTO.Name,
						NormalizedName = retailerDTO.Name.ToUpperInvariant(),
					};

					if (retailers.Any(p => p.NormalizedName == retailer.NormalizedName))
					{
						errors.Add(nameof(inputDTO.Retailers), new[] { "retailer name is already taken." });
						continue;
					}
				}
				else if (retailer is null)
				{
					errors.Add(nameof(inputDTO.Publishers), new[] { $"retailer '{retailerDTO.Id}' not found." });
					continue;
				}
				newGame.Retailers.Add(retailer);
			}


			if (string.IsNullOrWhiteSpace(inputDTO.Title))
			{
				errors.Add(nameof(inputDTO.Title), new[] { "title cannot be null or whitespace." });
			}
			else
			{
				newGame.Title = inputDTO.Title;
				newGame.NormalizedTitle = inputDTO.Title.ToUpperInvariant();
			}

			if (await context.Game.AnyAsync(g => g.NormalizedTitle == newGame.NormalizedTitle, cancellationToken) is true)
			{
				errors.Add(nameof(inputDTO.Title), new[] { "title is already taken." });
			}

			newGame.ReleaseDate = inputDTO.ReleaseDate;
			newGame.SupportsOnlinePlay = inputDTO.SupportsOnlinePlay;
			newGame.MaxPlayers = inputDTO.MaxPlayers;

			if (errors.Any())
			{
				return Results.BadRequest(new HttpValidationProblemDetails(errors));
			}

			await context.Game.AddAsync(newGame, cancellationToken);
			await context.SaveChangesAsync(cancellationToken);

			var dto = new GameListItemDTO(newGame);

			return Results.Created(BaseUrl + $"/{newGame.Id}", dto);
		});
		putGame
			.AllowAnonymous()
			.WithTags(nameof(GameModule))
			.Accepts<CreateGameDTO>(isOptional: false, "application/json")
			.Produces<GameListItemDTO>(200)
			.Produces<HttpValidationProblemDetails>(400)
			.Produces(404);

		var deleteGame = app.MapDelete($"{BaseUrl}/{{id:int}}", async (int id, GCPContext context, CancellationToken cancellationToken) =>
		{
			var game = await context.Game.Where(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
			if (game is null)
			{
				return Results.NotFound();
			}

			context.Remove(game);
			await context.SaveChangesAsync(cancellationToken);

			var dto = new GameListItemDTO(game);

			return Results.Ok(dto);
		});
		deleteGame
			.AllowAnonymous()
			.WithTags(nameof(GameModule))
			.Produces<GameListItemDTO>(200)
			.Produces(404);
	}
}
