using System.Globalization;

using EFCore.NamingConventions.Internal;

namespace GCP.Api.Utilities;

public static class StringHelper
{
	private static readonly SnakeCaseNameRewriter _snakeCaseNameRewriter = new(CultureInfo.InvariantCulture);

	public static string ToSnakeCase(string value)
	{
		return _snakeCaseNameRewriter.RewriteName(value);
	}
}
