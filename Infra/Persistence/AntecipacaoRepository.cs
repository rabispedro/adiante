using Core.Entities;
using Core.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infra.Persistence;

public class AntecipacaoRepository : IAntecipacaoRepository
{
	private SqlServerDbContext DbContext { get; set; }

	public AntecipacaoRepository(SqlServerDbContext dbContext)
	{
		DbContext = dbContext;
	}

	public async Task<Antecipacao> CreateAntecipacao(Antecipacao antecipacao)
	{
		DbContext.Antecipacoes.Add(antecipacao);
		await DbContext.SaveChangesAsync();

		return antecipacao;
	}

	public async Task<bool> ExistsAntecipacaoByCnpjWithinMonth(string cnpj, DateTime today)
	{
		return await DbContext.Antecipacoes
			.AnyAsync(antecipacao => antecipacao.Cnpj.Equals(cnpj) && antecipacao.Date.Month.CompareTo(today.Month) == 0);
	}
}
