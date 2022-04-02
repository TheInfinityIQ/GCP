using System.Globalization;

using EFCore.NamingConventions.Internal;

using GCP.Api.Data;
using GCP.Api.Data.Entities;
using GCP.Api.Data.Seeding;
using GCP.Api.Utilities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

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

builder.Services.AddSeeder();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
	app.UseMigrationsEndPoint();
}
else
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseSwagger();
app.UseSwaggerUI();

await app.RunSeederAsync();

app.Run();
