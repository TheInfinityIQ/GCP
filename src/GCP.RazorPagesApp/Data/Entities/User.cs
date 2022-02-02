
using Microsoft.AspNetCore.Identity;

namespace GCP.RazorPagesApp.Data.Entities;
public class User : IdentityUser<int>
{
	public string? DisplayName { get; set; }
}
