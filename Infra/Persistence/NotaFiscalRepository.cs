using Core.Entities;
using Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence;

public class NotaFiscalRepository : INotaFiscalRepository
{
	private SqlServerDbContext DbContext { get; set; }

	public NotaFiscalRepository(SqlServerDbContext dbContext)
	{
		DbContext = dbContext;
	}

	public async Task<NotaFiscal> CreateNotaFiscal(NotaFiscal notaFiscal)
	{
		DbContext.NotasFiscais.Add(notaFiscal);
		await DbContext.SaveChangesAsync();

		return notaFiscal;
	}

	public async Task<NotaFiscal> GetNotaFiscalByNumero(long numeroNotaFiscal)
	{
		return await DbContext.NotasFiscais
			.FirstOrDefaultAsync(notaFiscal => notaFiscal.Numero.Equals(numeroNotaFiscal));
	}
}
