using Core.Persistence;
using Infra.Persistence;

namespace Application.Extensions;

public static class RepositoryExtension
{
	public static void AddRepositories(this IServiceCollection service)
	{
		service.AddScoped<IEmpresaRepository, EmpresaRepository>();
		service.AddScoped<IAntecipacaoRepository, AntecipacaoRepository>();
		service.AddScoped<INotaFiscalRepository, NotaFiscalRepository>();
	}
}
