namespace GCP.RazorPagesApp.Data.Seeding;

public static class SeederExtensions
{
	public static IServiceCollection AddSeeder(this IServiceCollection services)
	{
		services.AddOptions<SeederOptions>()
			.BindConfiguration(SeederOptions.KEY)
			.ValidateDataAnnotations()
			.ValidateOnStart();

		services.AddScoped<ISeeder, Seeder>();

		return services;
	}

	public static async Task<IApplicationBuilder> RunSeederAsync(this IApplicationBuilder app, CancellationToken cancellationToken = default)
	{
		await using var scope = app.ApplicationServices.CreateAsyncScope();
		var seeder = scope.ServiceProvider.GetRequiredService<ISeeder>();
		await seeder.SeedAsync(cancellationToken);
		return app;
	}
}
