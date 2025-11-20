using Antecipacao;
using Empresa;

namespace API.Extension;

public static class ServiceExtension
{
	public static void AddServices(this IServiceCollection services)
	{
		services.AddScoped<EmpresaService>();
		services.AddScoped<AntecipacaoService>();
	}
}
