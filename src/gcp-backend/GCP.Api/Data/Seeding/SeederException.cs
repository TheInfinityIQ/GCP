namespace GCP.Api.Data.Seeding;

/// <summary>
/// Represents an error that occurred when seeding the database.
/// </summary>
public class SeederException : Exception
{
	public SeederException()
	{
	}

	public SeederException(string message)
		: base(message)
	{
	}

	public SeederException(string message, Exception innerException)
		: base(message, innerException)
	{
	}
}
