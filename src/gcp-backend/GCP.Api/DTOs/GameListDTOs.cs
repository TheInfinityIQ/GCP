using System.Text.Json.Serialization;

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
public record GameListSearchRequestDTO(
	[property: JsonIgnore] int? UserId,
	bool? HasDiscord = null,
	DateTimeOffset? ActiveFrom = null);
public record GameListDetailsRequestDTO(int Id);
public record GameListCreateRequestDTO(
	string Title,
	string? Description,
	bool VoteOncePerGame,
	bool IsPublic,
	int? UserLimit,
	[property: JsonIgnore] int OwnerId);
public record GameListUpdateRequestDTO(
	[property: JsonIgnore] int Id,
	string Title,
	string? Description,
	bool VoteOncePerGame,
	bool IsPublic,
	int? UserLimit,
	IEnumerable<int> Users,
	[property: JsonIgnore] int OwnerId);
public record GameListDeleteRequestDTO(
	int Id,
	[property: JsonIgnore] int OwnerId);
public record GameListsResponseDTO(IEnumerable<GameListDTO> GameLists);
