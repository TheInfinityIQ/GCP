﻿using System.Diagnostics;
using System.Text.Json;

using GCP.RazorPagesApp.Data.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace GCP.RazorPagesApp.Data.Seeding;

public class Seeder : ISeeder
{
	private readonly ILogger<Seeder> _logger;
	private readonly IOptions<SeederOptions> _options;
	private readonly GCPContext _context;
	private readonly UserManager<User> _userManager;
	private readonly RoleManager<Role> _roleManager;

	public ISet<DatabaseMigrationOption> DatabaseMigrationOptions => _options.Value.DatabaseMigrationOptions;

	public Seeder(ILogger<Seeder> logger, IOptions<SeederOptions> options, GCPContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
	{
		_logger = logger;
		_options = options;
		_context = context;
		_userManager = userManager;
		_roleManager = roleManager;
	}

	private void ValidateOptions()
	{
		if (string.IsNullOrWhiteSpace(_options.Value.SharedTestPassword))
		{
			var parameterName = nameof(_options.Value.SharedTestPassword);
			var error = new ArgumentException($"Provided '{parameterName}' cannot be null or whitespace.");
			_logger.LogError(error, "Provided '{parameterName}' cannot be null or whitespace.", parameterName);
			throw new SeederException("Error during seeding", error);
		}

		if (DatabaseMigrationOptions.Contains(DatabaseMigrationOption.Drop)
			&& !DatabaseMigrationOptions.Contains(DatabaseMigrationOption.Create)
			&& !DatabaseMigrationOptions.Contains(DatabaseMigrationOption.Migrate))
		{
			var parameterName = nameof(DatabaseMigrationOptions);
			var error = new ArgumentException($"Provided '{parameterName}' is invalid. If you choose drop, you must choose create or migrate as well.");
			_logger.LogError(error, "Provided '{parameterName}' is invalid. If you choose drop, you must choose create or migrate as well.", parameterName);
			throw new SeederException("Error during seeding", error);
		}
		else if (DatabaseMigrationOptions.Contains(DatabaseMigrationOption.Create)
			&& DatabaseMigrationOptions.Contains(DatabaseMigrationOption.Migrate))
		{
			var parameterName = nameof(DatabaseMigrationOptions);
			var error = new ArgumentException($"Provided '{parameterName}' is invalid. You cannot choose both create and migrate.");
			_logger.LogError(error, "Provided '{parameterName}' is invalid. You cannot choose both create and migrate.", parameterName);
			throw new SeederException("Error during seeding", error);
		}
	}

	private async Task HandleDatabaseMigrationOptionsAsync(CancellationToken cancellationToken = default)
	{
		if (DatabaseMigrationOptions?.Any() is null or false || DatabaseMigrationOptions.Contains(DatabaseMigrationOption.None))
		{
			return;
		}

		if (DatabaseMigrationOptions.Contains(DatabaseMigrationOption.Drop))
		{
			_logger.LogInformation("Dropping database...");
			await _context.Database.EnsureDeletedAsync(cancellationToken);
			_logger.LogInformation("Dropped database successfully.");
		}

		if (DatabaseMigrationOptions.Contains(DatabaseMigrationOption.Create))
		{
			_logger.LogInformation("Creating database...");
			await _context.Database.EnsureCreatedAsync(cancellationToken);
			_logger.LogInformation("Created database successfully.");
		}

		if (DatabaseMigrationOptions.Contains(DatabaseMigrationOption.Migrate))
		{
			var pendingMigrations = await _context.Database.GetPendingMigrationsAsync(cancellationToken);
			if (pendingMigrations.Any())
			{
				_logger.LogInformation("Migrating...");
				_logger.LogInformation("executing pending migrations:\n'{pendingMigrations}'", string.Join("',\n'", pendingMigrations));
				await _context.Database.MigrateAsync(cancellationToken);
				_logger.LogInformation("Executed pending migrations successfully.");
			}
			else
			{
				_logger.LogInformation("No pending migrations to execute against database.");
			}
		}
	}

	private async Task TestConnectionAsync(CancellationToken cancellationToken = default)
	{
		_logger.LogInformation("Testing database connection...");
		if (await _context.Database.CanConnectAsync(cancellationToken) is false)
		{
			var providerName = _context.Database.ProviderName;
			var error = new SeederException($"Error during seeding - Couldn't connect to '{providerName}' database to seed data. Please check connection string.");
			_logger.LogError(error, "Couldn't connect to '{providerName}' database to seed data. Please check connection string.", providerName);
			throw error;
		}
		_logger.LogInformation("Connection to database successfully.");
	}

	private async Task<Role> CreateRoleAsync(string name)
	{
		var role = new Role() { Name = name };
		var createRoleResult = await _roleManager.CreateAsync(role);
		if (!createRoleResult.Succeeded)
		{
			var errorDetails = JsonSerializer.Serialize(createRoleResult.Errors);
			var error = new SeederException($"Error during seeding - Failed to create '{name}' role. {errorDetails}");
			_logger.LogError(error, "Failed to create '{name}' role. {errorDetails}", name, errorDetails);
			throw error;
		}
		_logger.LogInformation("Created '{name}' role.", name);
		return role;
	}

	private async Task<User> CreateUserAsync(string email, string password, params string[] roles)
	{
		var joinedRoleNames = string.Join("','", roles);
		var user = new User() { Email = email, UserName = email };
		var createUserResult = await _userManager.CreateAsync(user, password);
		if (!createUserResult.Succeeded)
		{
			var errorDetails = JsonSerializer.Serialize(createUserResult.Errors);
			var error = new SeederException($"Error during seeding - Failed to create '{email}' user. {errorDetails}");
			_logger.LogError(error, "Failed to create '{email}' user. {errorDetails}", email, errorDetails);
			throw error;
		}
		_logger.LogInformation("Created '{email}' user.", email);

		var addToRolesResult = await _userManager.AddToRolesAsync(user, roles);
		if (!addToRolesResult.Succeeded)
		{
			var errorDetails = JsonSerializer.Serialize(addToRolesResult.Errors);
			var error = new SeederException($"Error during seeding - Failed to add '{email}' user to the following roles: '{joinedRoleNames}'. {errorDetails}");
			_logger.LogError(error, "Failed to add '{email}' user to the following roles: '{joinedRoleNames}'. {errorDetails}", email, joinedRoleNames, errorDetails);
			throw error;
		}
		_logger.LogInformation("Added '{email}' user to '{joinedRoleNames}' role(s).", email, joinedRoleNames);

		return user;
	}

	public async Task SeedAsync(CancellationToken cancellationToken = default)
	{
		if (_options.Value.SkipSeed)
		{
			return;
		}

		var sw = Stopwatch.StartNew();

		ValidateOptions();
		await HandleDatabaseMigrationOptionsAsync(cancellationToken);
		await TestConnectionAsync(cancellationToken);

		const string adminRoleName = "admin";
		const string moderatorRoleName = "moderator";

		_logger.LogInformation("Seeding role(s)...");
		if (await _context.Roles.AnyAsync(cancellationToken) is false)
		{
			await CreateRoleAsync(adminRoleName);
			await CreateRoleAsync(moderatorRoleName);
		}
		_logger.LogInformation("Seeded role(s) successfully.");

		_logger.LogInformation("Seeding user(s)...");
		if (await _context.Users.AnyAsync(cancellationToken) is false)
		{
			var adminRole = await _roleManager.FindByNameAsync(adminRoleName);
			var moderatorRole = await _roleManager.FindByNameAsync(moderatorRoleName);

			var admin = await CreateUserAsync("admin@gcp.com", _options.Value.SharedTestPassword, adminRole.Name);
			var moderator = await CreateUserAsync("mod@gcp.com", _options.Value.SharedTestPassword, moderatorRole.Name);

			admin.DisplayName = "Test admin user";
			moderator.DisplayName = "Test moderator user";
		}
		_logger.LogInformation("Seeded user(s) successfully.");


		_logger.LogInformation("Seeding game(s)...");
		if (await _context.Game.AnyAsync(cancellationToken) is false)
		{
			await _context.Game.AddRangeAsync(new Game[]
			{
				new() { GameLink = "https://store.steampowered.com/app/8930/Sid_Meiers_Civilization_V/", Score = 8, User = "Everett, Ian, James" },
				new() { GameLink = "https://store.steampowered.com/app/386070/Planetary_Annihilation_TITANS/", Score = 3, User = "Dawson" },
				new() { GameLink = "https://store.steampowered.com/app/294100/RimWorld/", Score = 5, User = "Everett, Ian, James" },
				new() { GameLink = "https://store.steampowered.com/app/8930/Tekken_7/", Score = 2, User = "Everett, Ian, James" },
			});
		}
		_logger.LogInformation("Seeded game(s).");


		_logger.LogInformation("Saving data...");
		await _context.SaveChangesAsync(cancellationToken);

		sw.Stop();
		_logger.LogInformation("Saved data successfully. ELAPSED: {time}", sw.Elapsed);
	}
}
