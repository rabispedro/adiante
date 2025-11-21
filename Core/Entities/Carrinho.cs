using System.Collections.Immutable;

namespace Core.Entities;

public class Carrinho
{
	public CNPJ Cnpj { get; init; }
	private ICollection<NotaFiscal> _notasFiscais;
	public DateTime DataCriacao { get; init; }

	public Carrinho()
	{
		_notasFiscais = new SortedSet<NotaFiscal>();
		DataCriacao = DateTime.Today;
	}

	public ICollection<NotaFiscal> GetNotasFiscais()
	{
		return _notasFiscais.ToImmutableSortedSet();
	}

	public void AddNotaFiscal(NotaFiscal notaFiscal)
	{
		_notasFiscais.Add(notaFiscal);
	}

	public void RemoveNotaFiscal(NotaFiscal notaFiscal)
	{
		_notasFiscais.Remove(notaFiscal);
	}
}
