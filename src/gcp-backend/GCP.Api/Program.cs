using System.Globalization;
using System.Text;

using EFCore.NamingConventions.Internal;

using GCP.Api.Data;
using GCP.Api.Data.Entities;
using GCP.Api.Data.Seeding;
using GCP.Api.DTOs;
using GCP.Api.Services;
using GCP.Api.Utilities;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
	var policy = new CorsPolicy();
	builder.Configuration.GetRequiredSection("CORS").Bind(policy);
	options.AddDefaultPolicy(policy);
});

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient();

builder.Services.AddControllers(options =>
{
	options.Filters.Add(new AuthorizeFilter());
});

builder.Services.AddDbContext<GCPContext>(options =>
{
	var connectionString = builder.Configuration.GetConnectionString("GCPContext")
		?? throw new InvalidOperationException("Connection string 'GCPContext' not found.");
	options.UseNpgsql(connectionString, npgsqlOptions =>
	{
		var snakeCaseNameRewriter = new SnakeCaseNameRewriter(CultureInfo.InvariantCulture);
		var migrationTableName = StringHelper.ToSnakeCase("__EFMigrationsHistory");
		npgsqlOptions.MigrationsHistoryTable(migrationTableName);
	});

	options.UseSnakeCaseNamingConvention();

#if DEBUG
	options.EnableDetailedErrors();
	options.EnableSensitiveDataLogging();
#endif
});

builder.Services.AddIdentity<User, Role>(options =>
	{
		options.SignIn.RequireConfirmedAccount = false;
	})
	.AddDefaultUI()
	.AddDefaultTokenProviders()
	.AddSignInManager()
	.AddUserManager<UserManager<User>>()
	.AddRoleManager<RoleManager<Role>>()
	.AddEntityFrameworkStores<GCPContext>();

builder.Services.AddAuthentication(options =>
	{
		options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	})
	.AddJwtBearer(options =>
	{
		var (validAudience, validIssuer, secretKey, _) = builder.Configuration.GetJwtOptions();
		var config = builder.Configuration;
		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

		options.SaveToken = true;
		options.RequireHttpsMetadata = false;
		options.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateIssuer = true,
			ValidateAudience = true,
			ValidAudience = validAudience,
			ValidIssuer = validIssuer,
			IssuerSigningKey = key,
		};
	});
builder.Services.AddAuthorization();

builder.Services.AddSeeder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
	var jwtScheme = new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = JwtBearerDefaults.AuthenticationScheme,
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter ‘Bearer’ [space] and then your valid token in the text input below.\r\n\r\nExample: 'Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9'",
	};
	swagger.AddSecurityDefinition(jwtScheme.Scheme, jwtScheme);
	swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		[new()
		{
			Reference = new OpenApiReference
			{
				Type = ReferenceType.SecurityScheme,
				Id = jwtScheme.Scheme,
			},
		}] = Array.Empty<string>(),
	});
});



builder.Services.AddScoped<ISteamSerivce, SteamSerivce>(sp =>
{
	IMemoryCache cache = sp.GetRequiredService<IMemoryCache>();
	var steamAppNameCache = cache.Get<SteamAppListDTO>(GCPConst.CacheKey.SteamAppNames);
	if (steamAppNameCache is null)
	{
		var fileInfo = new FileInfo("./steamAppList.json");
		if (fileInfo is { Exists: true, Length: > 0 })
		{
			var appListJson = File.ReadAllText(fileInfo.FullName);
			steamAppNameCache = SteamSerivce.ParseSteamAppNames(appListJson);
		}

		cache.Set(GCPConst.CacheKey.SteamAppNames, steamAppNameCache ??= new SteamAppListDTO(Array.Empty<SteamAppListItemDTO>()));
	}

	var ss = ActivatorUtilities.CreateInstance<SteamSerivce>(sp);
	ss.AddOrUpdateSteamAppNameCache(steamAppNameCache);
	return ss;
});
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IGameListSerivce, GameListSerivce>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler();
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.DisplayOperationId();
	options.DisplayRequestDuration();
	options.EnableTryItOutByDefault();
});

await app.RunSeederAsync();

app.Run();
