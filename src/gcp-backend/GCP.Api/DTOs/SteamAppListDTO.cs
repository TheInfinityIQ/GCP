namespace GCP.Api.DTOs;
public record SteamAppListDTO(IEnumerable<SteamAppListItemDTO> SteamApps);
public record SteamAppListItemDTO(long Id, string Name);
