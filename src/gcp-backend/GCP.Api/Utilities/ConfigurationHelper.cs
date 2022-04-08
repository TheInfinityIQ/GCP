namespace GCP.Api.Utilities;
public static class ConfigurationHelper
{
	public static bool TryGetSteamApiKey(this IConfiguration configuration!!, out string steamApiKey)
	{
		steamApiKey = configuration["SteamAPIKey"]!;
		return string.IsNullOrWhiteSpace(steamApiKey);
	}

	public static string GetSteamApiKey(this IConfiguration configuration!!)
		=> !TryGetSteamApiKey(configuration, out var apiKey)
			? throw new MissingConfigurationException("SteamAPIKey")
			: apiKey;

	public static (string ValidAudience, string ValidIssuer, string SecretKey, TimeSpan ExpiryTimeSpan) GetJwtOptions(this IConfiguration configuration!!)
	{
		var jwtConfig = configuration.GetRequiredSection("JWT");
		var validAudience = jwtConfig["ValidAudience"] ?? throw new MissingConfigurationException("JWT:ValidAudience");
		var validIssuer = jwtConfig["ValidIssuer"] ?? throw new MissingConfigurationException("JWT:ValidIssuer");
		var secretKey = jwtConfig["ValidIssuer"] ?? throw new MissingConfigurationException("JWT:SecretKey");
		return (validAudience, validIssuer, secretKey, jwtConfig.GetValue("ExpiryTimeSpan", TimeSpan.FromMinutes(30)));
	}
}


public class MissingConfigurationException : Exception
{
	public string ConfigurationPath { get; init; }

	public MissingConfigurationException(string configurationPath, string? message = null)
		: base(message ?? $"Missing '{configurationPath ?? "<unknown>"}' configuration.")
	{
		ConfigurationPath = configurationPath!;
	}
}
