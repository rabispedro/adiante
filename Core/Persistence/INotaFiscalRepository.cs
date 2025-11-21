using Core.Entities;

namespace Core.Persistence;

public interface INotaFiscalRepository
{
	Task<NotaFiscal> CreateNotaFiscal(NotaFiscal notaFiscal);
	Task<NotaFiscal> GetNotaFiscalByNumero(long numeroNotaFiscal);
}
