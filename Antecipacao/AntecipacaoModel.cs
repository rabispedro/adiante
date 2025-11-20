using System.Text.Json.Serialization;
using Empresa;

namespace Antecipacao;

public class AntecipacaoModel
{
	[JsonPropertyName("empresa")]
	public string Empresa { get; init; } = string.Empty;

	[JsonPropertyName("cnpj")]
	public string Cnpj { get; init; } = string.Empty;

	[JsonPropertyName("limite")]
	public decimal Limite { get; init; }

	[JsonIgnore]
	private ICollection<NotaFiscalModel> _notasFiscais = new SortedSet<NotaFiscalModel>();

	[JsonPropertyName("total_bruto")]
	public decimal TotalBruto { get; set; }

	[JsonPropertyName("total_liquido")]
	public decimal TotalLiquido { get; set; }

	[JsonIgnore]
	public DateOnly Date { get; init; } = DateOnly.FromDateTime(DateTime.Now);

	public void AddNotaFiscal(NotaFiscalModel NotaFiscal)
	{
		_notasFiscais.Add(NotaFiscal);
	}

	public void RemoveNotaFiscal(NotaFiscalModel NotaFiscal)
	{
		_notasFiscais.Remove(NotaFiscal);
	}

	public ICollection<NotaFiscalModel> GetNotasFiscais()
	{
		return [.. _notasFiscais];
	}
}
