using System.Collections.Immutable;

namespace Core.Entities;

public class Carrinho
{
	public CNPJ Cnpj { get; init; }
	public ICollection<NotaFiscal> NotasFiscais { get; init; } = new LinkedList<NotaFiscal>();
	public DateTime DataCriacao { get; init; } = DateTime.Now;

	public ICollection<NotaFiscal> GetNotasFiscais()
	{
		return NotasFiscais.ToImmutableList();
	}

	public void AddNotaFiscal(NotaFiscal notaFiscal)
	{
		NotasFiscais.Add(notaFiscal);
	}

	public void RemoveNotaFiscal(NotaFiscal notaFiscal)
	{
		NotasFiscais.Remove(notaFiscal);
	}
}
