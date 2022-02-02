
using GCP.RazorPagesApp.Api.Module;

using Microsoft.Extensions.DependencyInjection.Extensions;

namespace GCP.RazorPagesApp;
public static class MinimalApi
{
	private static readonly Type _moduleInterfaceType = typeof(IModule);
	private static readonly Type _baseModuleType = typeof(BaseModule);
	private static readonly IReadOnlyList<Type> _moduleTypes = _moduleInterfaceType.Assembly.GetExportedTypes()
		.Where(t => t.IsAssignableTo(_moduleInterfaceType))
		.Where(t => t != _moduleInterfaceType && t != _baseModuleType)
		.ToArray();

	public static IServiceCollection AddMinimalApiServices(this IServiceCollection services)
	{
		foreach (var moduleType in _moduleTypes)
		{
			services.TryAddTransient(_moduleInterfaceType, moduleType);
		}

		var sp = services.BuildServiceProvider();
		foreach (var moduleType in _moduleTypes)
		{
			var module = (IModule)ActivatorUtilities.GetServiceOrCreateInstance(sp, moduleType);
			module.AddServices(services);
		}

		return services;
	}

	public static IEndpointRouteBuilder MapMinimalApiEndpoints(this IEndpointRouteBuilder app)
	{
		var sp = app.ServiceProvider;
		foreach (var moduleType in _moduleTypes)
		{
			var module = (IModule)ActivatorUtilities.GetServiceOrCreateInstance(sp, moduleType);
			module.MapEndpoints(app);
		}

		return app;
	}
}

