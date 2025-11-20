using System.Collections.Immutable;

namespace Empresa;

public class CarrinhoModel
{
	public CNPJ Cnpj { get; init; }
	private ICollection<NotaFiscalModel> _notasFiscais;
	public DateOnly DataCriacao { get; init; }

	public CarrinhoModel()
	{
		_notasFiscais = new SortedSet<NotaFiscalModel>();
		DataCriacao = DateOnly.FromDateTime(DateTime.Now);
	}

	public ICollection<NotaFiscalModel> GetNotasFiscais()
	{
		return _notasFiscais.ToImmutableSortedSet();
	}

	public void AddNotaFiscal(NotaFiscalModel notaFiscal)
	{
		_notasFiscais.Add(notaFiscal);
	}

	public void RemoveNotaFiscal(NotaFiscalModel notaFiscal)
	{
		_notasFiscais.Remove(notaFiscal);
	}
}
