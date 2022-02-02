namespace GCP.RazorPagesApp.Api.Module.GameModule;

public record GameListItemDTO(string Title, string Link, int Score, string User)
{
	public GameListItemDTO(Data.Entities.Game game)
		: this(
			Title: DetermineTitleFromLink(game.GameLink ?? ""),
			Link: game.GameLink ?? "",
			Score: game.Score,
			User: game.User ?? ""
		)
	{
	}

	public static string DetermineTitleFromLink(string link)
		=> link
		.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
		.LastOrDefault()
		?.Replace('_', ' ')
		?? "";
}

public record CreateGameDTO(string Link, int Score, string User)
{
	public static string DetermineTitleFromLink(string link)
		=> link
		.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
		.LastOrDefault()
		?.Replace('_', ' ')
		?? "";
}
