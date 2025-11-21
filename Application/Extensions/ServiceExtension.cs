using Core.Services;

namespace Application.Extensions;

public static class ServiceExtension
{
	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<EmpresaService>();
		services.AddScoped<AntecipacaoService>();
	}
}
