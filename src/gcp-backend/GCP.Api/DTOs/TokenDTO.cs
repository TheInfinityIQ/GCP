namespace GCP.Api.DTOs;

public record TokenRequestDTO(string Email, string Password, bool RememberMe = false);
public record TokenResponseDTO
{
	//[JsonPropertyName("access_token")]
	public string AccessToken { get; init; } = default!;

	//[JsonPropertyName("expiries_on")]
	public DateTimeOffset ExpiriesOn { get; init; }
}
