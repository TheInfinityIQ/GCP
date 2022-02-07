namespace GCP.RazorPagesApp.Data.Entities;

public class Retailer
{
	public int Id { get; set; }
	public string Name { get; set; } = default!;
	public string NormalizedName { get; set; } = default!;

	public List<Game> Games { get; set; } = new List<Game>();
	public List<GameRetailer> GameRetailers { get; set; } = new List<GameRetailer>();
}
