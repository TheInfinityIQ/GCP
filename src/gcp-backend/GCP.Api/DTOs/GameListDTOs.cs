namespace GCP.Api.DTOs;
public record GameListDTO(
	int Id,
	UserDisplayNameDTO Owner,
	string Title,
	string? Description,
	bool VoteOncePerGame,
	bool IsPublic,
	int? UserLimit,
	DateTimeOffset CreatedOnUtc,
	DateTimeOffset? LastUpdatedOnUtc,
	IEnumerable<UserDisplayNameDTO> Users);
public record GameListCreateRequestDTO(
	string Title,
	string? Description,
	bool VoteOncePerGame,
	bool IsPublic,
	int? UserLimit);
public record GameListUpdateRequestDTO(
	string Title,
	string? Description,
	bool VoteOncePerGame,
	bool IsPublic,
	int? UserLimit,
	IEnumerable<int> Users);
public record GameListsResponseDTO(IEnumerable<GameListDTO> GameLists);
