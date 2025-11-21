using Core.Entities;
using Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence;

public class EmpresaRepository : IEmpresaRepository
{
	private SqlServerDbContext DbContext { get; set; }

	public EmpresaRepository(SqlServerDbContext dbContext)
	{
		DbContext = dbContext;
	}

	public async Task<Empresa> CreateEmpresa(Empresa empresa)
	{
		DbContext.Empresas.Add(empresa);
		await DbContext.SaveChangesAsync();

		return empresa;
	}

	public async Task<Empresa> GetEmpresaByCnpj(string cnpj)
	{
		return await DbContext.Empresas.FirstOrDefaultAsync(empresa => empresa.Cnpj.Equals(cnpj));
	}
}
