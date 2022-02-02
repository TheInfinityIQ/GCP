namespace GCP.RazorPagesApp.Api.Module;
public interface IModule
{
	void AddServices(IServiceCollection services);
	void MapEndpoints(IEndpointRouteBuilder app);
}

public abstract class BaseModule : IModule
{
	public virtual string RoutePrefix { get; } = "/api";
	public abstract void AddServices(IServiceCollection services);
	public abstract void MapEndpoints(IEndpointRouteBuilder app);
}
