namespace GCP.Api.Utilities;
public static class GCPConst
{
	public static class CacheKey
	{
		public const string SteamAppNames = "steamAppNames";
		public const string SteamAppDetails = "steamAppDetails_{0}";
		public static string GetSteamAppDetailsKey(long appId) => string.Format(SteamAppDetails, appId);
		public static string GetSteamAppDetailsKey(ulong appId) => string.Format(SteamAppDetails, appId);
	}
}
