using System.Text.Json.Serialization;

namespace GCP.Api.DTOs;

public record SteamAppDetailsDTO(
	[property: JsonPropertyName("type")] string Type,
	[property: JsonPropertyName("name")] string Name,
	[property: JsonPropertyName("steam_appid")] int SteamAppid,
	[property: JsonPropertyName("required_age")] string RequiredAge,
	[property: JsonPropertyName("is_free")] bool IsFree,
	[property: JsonPropertyName("dlc")] IReadOnlyList<int> Dlc,
	[property: JsonPropertyName("detailed_description")] string DetailedDescription,
	[property: JsonPropertyName("about_the_game")] string AboutTheGame,
	[property: JsonPropertyName("short_description")] string ShortDescription,
	[property: JsonPropertyName("supported_languages")] string SupportedLanguages,
	[property: JsonPropertyName("header_image")] string HeaderImage,
	[property: JsonPropertyName("website")] string Website,
	//[property: JsonPropertyName("pc_requirements")] PcRequirements PcRequirements,
	//[property: JsonPropertyName("mac_requirements")] MacRequirements MacRequirements,
	//[property: JsonPropertyName("linux_requirements")] LinuxRequirements LinuxRequirements,
	[property: JsonPropertyName("legal_notice")] string LegalNotice,
	[property: JsonPropertyName("developers")] IReadOnlyList<string> Developers,
	[property: JsonPropertyName("publishers")] IReadOnlyList<string> Publishers,
	[property: JsonPropertyName("price_overview")] PriceOverview PriceOverview,
	//[property: JsonPropertyName("packages")] IReadOnlyList<int> Packages,
	//[property: JsonPropertyName("package_groups")] IReadOnlyList<PackageGroup> PackageGroups,
	[property: JsonPropertyName("platforms")] Platforms Platforms,
	[property: JsonPropertyName("metacritic")] Metacritic Metacritic,
	[property: JsonPropertyName("categories")] IReadOnlyList<Category> Categories,
	[property: JsonPropertyName("genres")] IReadOnlyList<Genre> Genres,
	[property: JsonPropertyName("screenshots")] IReadOnlyList<Screenshot> Screenshots,
	[property: JsonPropertyName("movies")] IReadOnlyList<Movy> Movies,
	//[property: JsonPropertyName("recommendations")] Recommendations Recommendations,
	//[property: JsonPropertyName("achievements")] Achievements Achievements,
	[property: JsonPropertyName("release_date")] ReleaseDate ReleaseDate,
	//[property: JsonPropertyName("support_info")] SupportInfo SupportInfo,
	[property: JsonPropertyName("background")] string Background,
	[property: JsonPropertyName("background_raw")] string BackgroundRaw//,
	//[property: JsonPropertyName("content_descriptors")] ContentDescriptors ContentDescriptors
);

public record PcRequirements(
	[property: JsonPropertyName("minimum")] string Minimum,
	[property: JsonPropertyName("recommended")] string Recommended
);

public record MacRequirements(
	[property: JsonPropertyName("minimum")] string Minimum,
	[property: JsonPropertyName("recommended")] string Recommended
);

public record LinuxRequirements(
	[property: JsonPropertyName("minimum")] string Minimum,
	[property: JsonPropertyName("recommended")] string Recommended
);

public record PriceOverview(
	[property: JsonPropertyName("currency")] string Currency,
	[property: JsonPropertyName("initial")] int Initial,
	[property: JsonPropertyName("final")] int Final,
	[property: JsonPropertyName("discount_percent")] int DiscountPercent,
	[property: JsonPropertyName("initial_formatted")] string InitialFormatted,
	[property: JsonPropertyName("final_formatted")] string FinalFormatted
);

public record Sub(
	[property: JsonPropertyName("packageid")] int Packageid,
	[property: JsonPropertyName("percent_savings_text")] string PercentSavingsText,
	[property: JsonPropertyName("percent_savings")] int PercentSavings,
	[property: JsonPropertyName("option_text")] string OptionText,
	[property: JsonPropertyName("option_description")] string OptionDescription,
	[property: JsonPropertyName("can_get_free_license")] string CanGetFreeLicense,
	[property: JsonPropertyName("is_free_license")] bool IsFreeLicense,
	[property: JsonPropertyName("price_in_cents_with_discount")] int PriceInCentsWithDiscount
);

public record PackageGroup(
	[property: JsonPropertyName("name")] string Name,
	[property: JsonPropertyName("title")] string Title,
	[property: JsonPropertyName("description")] string Description,
	[property: JsonPropertyName("selection_text")] string SelectionText,
	[property: JsonPropertyName("save_text")] string SaveText,
	[property: JsonPropertyName("display_type")] int DisplayType,
	[property: JsonPropertyName("is_recurring_subscription")] string IsRecurringSubscription,
	[property: JsonPropertyName("subs")] IReadOnlyList<Sub> Subs
);

public record Platforms(
	[property: JsonPropertyName("windows")] bool Windows,
	[property: JsonPropertyName("mac")] bool Mac,
	[property: JsonPropertyName("linux")] bool Linux
);

public record Metacritic(
	[property: JsonPropertyName("score")] int Score,
	[property: JsonPropertyName("url")] string Url
);

public record Category(
	[property: JsonPropertyName("id")] int Id,
	[property: JsonPropertyName("description")] string Description
);

public record Genre(
	[property: JsonPropertyName("id")] string Id,
	[property: JsonPropertyName("description")] string Description
);

public record Screenshot(
	[property: JsonPropertyName("id")] int Id,
	[property: JsonPropertyName("path_thumbnail")] string PathThumbnail,
	[property: JsonPropertyName("path_full")] string PathFull
);

public record Webm(
	[property: JsonPropertyName("480")] string _480,
	[property: JsonPropertyName("max")] string Max
);

public record Mp4(
	[property: JsonPropertyName("480")] string _480,
	[property: JsonPropertyName("max")] string Max
);

public record Movy(
	[property: JsonPropertyName("id")] int Id,
	[property: JsonPropertyName("name")] string Name,
	[property: JsonPropertyName("thumbnail")] string Thumbnail,
	[property: JsonPropertyName("webm")] Webm Webm,
	[property: JsonPropertyName("mp4")] Mp4 Mp4,
	[property: JsonPropertyName("highlight")] bool Highlight
);

public record Recommendations(
	[property: JsonPropertyName("total")] int Total
);

public record Highlighted(
	[property: JsonPropertyName("name")] string Name,
	[property: JsonPropertyName("path")] string Path
);

public record Achievements(
	[property: JsonPropertyName("total")] int Total,
	[property: JsonPropertyName("highlighted")] IReadOnlyList<Highlighted> Highlighted
);

public record ReleaseDate(
	[property: JsonPropertyName("coming_soon")] bool ComingSoon,
	[property: JsonPropertyName("date")] string Date
);

public record SupportInfo(
	[property: JsonPropertyName("url")] string Url,
	[property: JsonPropertyName("email")] string Email
);

public record ContentDescriptors(
	[property: JsonPropertyName("ids")] IReadOnlyList<object> Ids,
	[property: JsonPropertyName("notes")] object Notes
);
