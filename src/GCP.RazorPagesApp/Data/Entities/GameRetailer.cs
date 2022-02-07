namespace GCP.RazorPagesApp.Data.Entities;

public class GameRetailer
{
	public int GameId { get; set; }
	public Game Game { get; set; } = default!;

	public int RetailerId { get; set; }
	public Retailer Retailer { get; set; } = default!;
}
