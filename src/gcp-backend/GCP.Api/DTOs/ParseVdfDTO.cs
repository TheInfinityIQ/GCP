namespace GCP.Api.DTOs;
public record ParseVdfRequestDTO(int? CurrentUserId, Stream VDFStream);
public record ParseVdfResponseDTO(IEnumerable<ParsedSteamAppNameDTO> SteamAppNames);
public record ParsedSteamAppNameDTO(long AppId, string AppName);
