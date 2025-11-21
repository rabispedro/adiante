using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Core.Entities;

public class NotaFiscal
{
	[JsonIgnore]
	public Empresa Empresa { get; set; }

	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	[JsonPropertyName("numero")]
	public long Numero { get; init; }

	[JsonPropertyName("valor")]
	public decimal Valor { get; init; }

	[JsonPropertyName("data_vencimento")]
	public DateTime DataVencimento { get; set; }

	public static bool IsValid(NotaFiscal notaFiscal)
	{
		if (notaFiscal == null || notaFiscal.DataVencimento == null || notaFiscal.DataVencimento.CompareTo(DateTime.Today) < 0)
		{
			return false;
		}

		return true;
	}
}
