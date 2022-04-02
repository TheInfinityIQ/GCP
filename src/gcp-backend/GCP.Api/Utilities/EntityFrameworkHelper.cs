
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GCP.Api.Utilities;

public static class EntityFrameworkHelper
{
	public static void ToTableSnakeCase<T>(this EntityTypeBuilder<T> builder)
		where T : class
	{
		var tableName = builder.Metadata.GetTableName() ?? builder.Metadata.GetDefaultTableName() ?? builder.Metadata.ShortName();
		var schema = builder.Metadata.GetSchema() ?? builder.Metadata.GetDefaultSchema()!;
		builder.ToTableSnakeCase(tableName, schema);
	}

	public static void ToTableSnakeCaseDefaultTableName<T>(this EntityTypeBuilder<T> builder)
		where T : class
	{
		var tableName = builder.Metadata.GetDefaultTableName() ?? builder.Metadata.ShortName();
		var schema = builder.Metadata.GetSchema() ?? builder.Metadata.GetDefaultSchema()!;
		builder.ToTableSnakeCase(tableName, schema);
	}

	public static void ToTableSnakeCase<T>(this EntityTypeBuilder<T> builder, string tableName)
		where T : class
	{
		if (string.IsNullOrWhiteSpace(tableName))
		{
			throw new ArgumentException($"'{nameof(tableName)}' cannot be null or whitespace.", nameof(tableName));
		}

		builder.ToTable(tb =>
		{
			tb.Metadata.SetTableName(StringHelper.ToSnakeCase(tableName));
		});
	}

	public static void ToTableSnakeCase<T>(this EntityTypeBuilder<T> builder, string tableName, string schema)
		where T : class
	{
		if (string.IsNullOrWhiteSpace(tableName))
		{
			throw new ArgumentException($"'{nameof(tableName)}' cannot be null or whitespace.", nameof(tableName));
		}

		if (string.IsNullOrWhiteSpace(schema))
		{
			throw new ArgumentException($"'{nameof(schema)}' cannot be null or whitespace.", nameof(schema));
		}

		builder.ToTable(tb =>
		{
			tb.Metadata.SetTableName(StringHelper.ToSnakeCase(tableName));
			tb.Metadata.SetSchema(StringHelper.ToSnakeCase(schema));
		});
	}
}
