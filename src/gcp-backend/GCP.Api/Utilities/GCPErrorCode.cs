using System.Net;

namespace GCP.Api.Utilities;

public enum GCPErrorCode
{
	// Generic errors
	BadRequest = HttpStatusCode.BadRequest,
	InternalServerError = HttpStatusCode.InternalServerError,
	Unauthorized = HttpStatusCode.Unauthorized,
	Forbidden = HttpStatusCode.Forbidden,
	NotFound = HttpStatusCode.NotFound,
	Conflict = HttpStatusCode.Conflict,
	NotImplemented = HttpStatusCode.NotImplemented,


	// Domain errors
	EmptyVdfFile = GCPErrorCodeExtensions.DomainErrorStartingPoint,
	FailedToParseGameLibraryVdfFile,
	FailedToParseGameDetails,
	FailedToGetSteamAppList,

	EmailIsAlreadyTaken,
	DisplayNameIsAlreadyTaken,

	MissingConfiguration,

	TitleIsAlreadyTaken,
}

public static class GCPErrorCodeExtensions
{
	public const int DomainErrorStartingPoint = 1_000;
	public static EventId ToEventId(this GCPErrorCode errorCode) => new((int)errorCode, Enum.GetName(errorCode));
	public static HttpStatusCode ToHttpStatusCode(this GCPErrorCode errorCode) => errorCode switch
	{
		GCPErrorCode.Unauthorized => HttpStatusCode.Unauthorized,
		GCPErrorCode.Forbidden => HttpStatusCode.Forbidden,
		GCPErrorCode.NotFound => HttpStatusCode.NotFound,
		GCPErrorCode.Conflict => HttpStatusCode.Conflict,
		GCPErrorCode.NotImplemented => HttpStatusCode.NotImplemented,
		GCPErrorCode.InternalServerError => HttpStatusCode.InternalServerError,
		_ => HttpStatusCode.BadRequest,
	};
}
