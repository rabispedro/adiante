using System.Text.Json.Serialization;

namespace Empresa;

public class NotaFiscalModel
{
	[JsonIgnore]
	public EmpresaModel Empresa { get; set; }

	[JsonPropertyName("numero")]
	public long Numero { get; init; }

	[JsonPropertyName("valor")]
	public decimal Valor { get; init; }

	[JsonPropertyName("data_vencimento")]
	public DateOnly DataVencimento { get; set; }

	public static bool IsValid(NotaFiscalModel notaFiscal)
	{
		if (notaFiscal == null || notaFiscal.DataVencimento == null || notaFiscal.DataVencimento.CompareTo(DateOnly.FromDateTime(DateTime.Now)) < 0)
		{
			return false;
		}

		return true;
	}
}
