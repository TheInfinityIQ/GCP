
using Microsoft.AspNetCore.Identity;

namespace GCP.Api.Data.Entities;
public class User : IdentityUser<int>
{
	public string DisplayName { get; set; } = default!;
}
