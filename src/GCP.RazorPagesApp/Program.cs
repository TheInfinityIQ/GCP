
using System.Globalization;

using EFCore.NamingConventions.Internal;

using GCP.RazorPagesApp;
using GCP.RazorPagesApp.Data;
using GCP.RazorPagesApp.Data.Entities;
using GCP.RazorPagesApp.Data.Seeding;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<GCPContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("GCPContext"), npgsqlOptions =>
	{
		var snakeCaseNameRewriter = new SnakeCaseNameRewriter(CultureInfo.InvariantCulture);
		var migrationTableName = snakeCaseNameRewriter.RewriteName("__EFMigrationsHistory");
		npgsqlOptions.MigrationsHistoryTable(migrationTableName);
	});

	options.UseSnakeCaseNamingConvention();
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

builder.Services.AddMinimalApiServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
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
app.MapMinimalApiEndpoints();
app.UseSwaggerUI();

await app.RunSeederAsync();

app.Run();
