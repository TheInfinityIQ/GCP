using System.Collections.Immutable;

namespace GCP.Api.Utilities;
public record GCPResult
{
	protected readonly List<GCPError> _errors = new();
	protected readonly HashSet<object> _metadata = new();

	public bool Succeeded => !Failed;
	public bool Failed => Errors?.Count > 0;

	public IImmutableSet<object> Metadata => ImmutableHashSet.CreateRange(_metadata);
	public IImmutableList<GCPError> Errors => ImmutableList.CreateRange(_errors ?? (IEnumerable<GCPError>)Array.Empty<GCPError>());

	public GCPResult()
	{
	}

	public GCPResult(GCPError error)
	{
		_errors.Add(error);
	}

	public GCPResult(IEnumerable<GCPError> errors)
	{
		_errors.AddRange(errors);
	}

	public GCPResult(GCPErrorCode errorCode)
	{
		_errors.Add(new(errorCode));
	}

	public static GCPResult Success() => new();
	public static GCPResult Failure(GCPError error) => new(error);
	public static GCPResult Failure(IEnumerable<GCPError> errors) => new(errors);
	public static GCPResult Failure(GCPErrorCode errorCode) => new(errorCode);
	public static GCPResult Failure(GCPErrorCode errorCode, string errorMessage) => new(new GCPError(errorCode, errorMessage));


	public static GCPResult<T> Success<T>(T content) => new(content);
	public static GCPResult<T> Failure<T>(GCPError error) => new(error);
	public static GCPResult<T> Failure<T>(IEnumerable<GCPError> errors) => new(errors);
	public static GCPResult<T> Failure<T>(GCPErrorCode errorCode) => new(errorCode);
	public static GCPResult<T> Failure<T>(GCPErrorCode errorCode, string errorMessage) => new(new GCPError(errorCode, errorMessage));
}

public record GCPResult<T> : GCPResult
{
	public T? Content { get; init; }

	public GCPResult(T content)
	{
		Content = content;
	}

	public GCPResult(GCPError error)
		: base(error)
	{
	}

	public GCPResult(IEnumerable<GCPError> errors)
		: base(errors)
	{
	}

	public GCPResult(GCPErrorCode errorCode)
		: base(errorCode)
	{
	}
}


public static class GCPResultExtensions
{
	public static GCPResult Combine(this GCPResult result, GCPResult other) => new(result.Errors.AddRange(other.Errors));
	public static GCPResult Combine(this GCPResult result, IEnumerable<GCPResult> others) => new(result.Errors.AddRange(others.SelectMany(x => x.Errors)));

	public static GCPResult<T> CombineErrors<T>(this GCPResult<T> result, GCPResult other) => new(result.Errors.AddRange(other.Errors));
	public static GCPResult<T> CombineErrors<T>(this GCPResult<T> result, IEnumerable<GCPResult> others) => new(result.Errors.AddRange(others.SelectMany(x => x.Errors)));


	public static GCPResult DiscardContent<T>(this GCPResult<T> result)
	{
		if (result.Content is IDisposable disposable)
		{
			disposable.Dispose();
		}
		return new(result.Errors);
	}

	public static T? Unwrap<T>(GCPResult<T> result) => result.Content;

	public static GCPResult WithMetadata(this GCPResult result, object metadata)
	{
		result.Metadata.Add(metadata);
		return result;
	}
	public static GCPResult<T> WithMetadata<T>(this GCPResult<T> result, object metadata)
	{
		result.Metadata.Add(metadata);
		return result;
	}

	public static GCPResult WithMetadata(this GCPResult result, IDictionary<string, object> metadata)
	{
		result.Metadata.Add(metadata);
		return result;
	}

	public static GCPResult<T> WithMetadata<T>(this GCPResult<T> result, IDictionary<string, object> metadata)
	{
		result.Metadata.Add(metadata);
		return result;
	}
}

public record GCPError(GCPErrorCode Code, string? Message = null);
