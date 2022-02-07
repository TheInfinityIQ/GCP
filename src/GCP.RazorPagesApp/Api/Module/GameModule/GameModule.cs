using GCP.RazorPagesApp.Data;

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

		//var postGame = app.MapPost(BaseUrl, async (CreateGameDTO inputDTO, GCPContext context, CancellationToken cancellationToken) =>
		//{
		//	var (link, score, user) = inputDTO;

		//	var errors = new Dictionary<string, string[]>();
		//	if (!Uri.TryCreate(link, UriKind.Absolute, out _))
		//	{
		//		errors.Add(nameof(link), new[] { "must be a valid absolute link." });
		//	}

		//	if (score is < 0)
		//	{
		//		errors.Add(nameof(score), new[] { "can't be less than zero." });
		//	}

		//	if (string.IsNullOrWhiteSpace(user))
		//	{
		//		errors.Add(nameof(user), new[] { "cannot be null or whitespace." });
		//	}

		//	if (errors.Any())
		//	{
		//		return Results.BadRequest(new HttpValidationProblemDetails(errors));
		//	}

		//	var game = new Data.Entities.Game { Score = score, GameLink = link, User = user };

		//	await context.Game.AddAsync(game, cancellationToken);
		//	await context.SaveChangesAsync(cancellationToken);

		//	var dto = new GameListItemDTO(game);

		//	return Results.Created(BaseUrl + $"/{game.Id}", dto);
		//});
		//postGame
		//	.AllowAnonymous()
		//	.WithTags(nameof(GameModule))
		//	.Accepts<CreateGameDTO>(isOptional: false, "application/json")
		//	.Produces<GameListItemDTO>(200)
		//	.Produces<HttpValidationProblemDetails>(400)
		//	.Produces(404);

		//var putGame = app.MapPut($"{BaseUrl}/{{id:int}}", async (int id, CreateGameDTO inputDTO, GCPContext context, CancellationToken cancellationToken) =>
		//{
		//	var game = await context.Game.Where(x => x.Id == id).SingleOrDefaultAsync(cancellationToken);
		//	if (game is null)
		//	{
		//		return Results.NotFound();
		//	}

		//	var (link, score, user) = inputDTO;

		//	var errors = new Dictionary<string, string[]>();
		//	if (!Uri.TryCreate(link, UriKind.Absolute, out _))
		//	{
		//		errors.Add(nameof(link), new[] { "must be a valid absolute link." });
		//	}

		//	if (score is < 0)
		//	{
		//		errors.Add(nameof(score), new[] { "can't be less than zero." });
		//	}

		//	if (string.IsNullOrWhiteSpace(user))
		//	{
		//		errors.Add(nameof(user), new[] { "cannot be null or whitespace." });
		//	}

		//	if (errors.Any())
		//	{
		//		return Results.BadRequest(new HttpValidationProblemDetails(errors));
		//	}

		//	game.GameLink = link;
		//	game.Score = score;
		//	game.User = user;

		//	context.Game.Update(game);
		//	await context.SaveChangesAsync(cancellationToken);

		//	var dto = new GameListItemDTO(game);

		//	return Results.Ok(dto);
		//});
		//putGame
		//	.AllowAnonymous()
		//	.WithTags(nameof(GameModule))
		//	.Accepts<CreateGameDTO>(isOptional: false, "application/json")
		//	.Produces<GameListItemDTO>(200)
		//	.Produces<HttpValidationProblemDetails>(400)
		//	.Produces(404);

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
