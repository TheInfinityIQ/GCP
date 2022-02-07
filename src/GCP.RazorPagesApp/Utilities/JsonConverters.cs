using System.Text.Json;
using System.Text.Json.Serialization;

namespace GCP.RazorPagesApp.Utilities;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
	public const string FORMAT = "yyyy-MM-dd";
	private readonly string _serializationFormat;

	public DateOnlyJsonConverter()
		: this(null)
	{
	}

	public DateOnlyJsonConverter(string? serializationFormat)
	{
		_serializationFormat = serializationFormat ?? FORMAT;
	}

	public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> DateOnly.Parse(reader.GetString()!);


	public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToString(_serializationFormat));
}

public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
{
	public const string FORMAT = "HH:mm:ss.fff";
	private readonly string _serializationFormat;

	public TimeOnlyJsonConverter()
		: this(null)
	{
	}

	public TimeOnlyJsonConverter(string? serializationFormat)
	{
		_serializationFormat = serializationFormat ?? FORMAT;
	}

	public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		=> TimeOnly.Parse(reader.GetString()!);

	public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options)
		=> writer.WriteStringValue(value.ToString(_serializationFormat));
}
