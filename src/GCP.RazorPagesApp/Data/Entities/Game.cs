namespace GCP.RazorPagesApp.Data.Entities;

public class Game
{
	public int Id { get; set; }
	public string? GameLink { get; set; }
	public int Score { get; set; }
	public string? User { get; set; }
}
