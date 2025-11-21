using Core.Entities;

namespace Core.Persistence;

public interface IAntecipacaoRepository
{
	Task<Antecipacao> CreateAntecipacao(Antecipacao antecipacao);
	Task<bool> ExistsAntecipacaoByCnpjWithinMonth(string cnpj, DateTime today);
}
