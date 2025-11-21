using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Entities;

public class Antecipacao
{
	[JsonPropertyName("empresa")]
	public string Empresa { get; init; } = string.Empty;

	[Key]
	[JsonPropertyName("cnpj")]
	public string Cnpj { get; init; } = string.Empty;

	[JsonPropertyName("limite")]
	public decimal Limite { get; init; }


	[JsonPropertyName("notas_fiscais")]
	public ICollection<NotaFiscalCheckout> NotasFiscais = new SortedSet<NotaFiscalCheckout>();


	[JsonPropertyName("total_bruto")]
	public decimal TotalBruto { get; set; }


	[JsonPropertyName("total_liquido")]
	public decimal TotalLiquido { get; set; }


	[JsonIgnore]
	public DateTime Date { get; init; } = DateTime.Today;

	public void AddNotaFiscal(NotaFiscalCheckout NotaFiscal)
	{
		NotasFiscais.Add(NotaFiscal);
	}

	public void RemoveNotaFiscal(NotaFiscalCheckout NotaFiscal)
	{
		NotasFiscais.Remove(NotaFiscal);
	}
}
