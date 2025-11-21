using System.Text.Json.Serialization;

namespace Core.Entities;

public class NotaFiscalCheckout
{
	[JsonPropertyName("numero")]
	public long Numero { get; init; }

	[JsonPropertyName("valor_bruto")]
	public decimal ValorBruto { get; set; }

	[JsonPropertyName("valor_liquido")]
	public decimal ValorLiquido { get; set; }
}
