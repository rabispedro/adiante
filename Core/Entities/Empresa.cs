using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Empresa
{
	[Key]
	public string Cnpj { get; init; } = string.Empty;
	public string Nome { get; init; } = string.Empty;
	public decimal Faturamento { get; init; }
	public RamoEmpresa Ramo { get; init; }

	public enum RamoEmpresa
	{
		SERVICOS,
		PRODUTOS
	}

	public static bool IsValid(Empresa empresa)
	{
		return (empresa != null) && !string.IsNullOrWhiteSpace(empresa.Nome) && CNPJ.IsValid(empresa.Cnpj);
	}

	// NOTE: calcular limite inteiro dentro da empresa
	public decimal GetLimite()
	{
		decimal percent;

		if (Faturamento < 10000.00M)
		{
			percent = 0.00M;
		}
		else if (Faturamento < 50000.00M)
		{
			percent = 0.50M;
		}
		else if (Faturamento < 100000.00M)
		{
			percent = Ramo == RamoEmpresa.SERVICOS ? 0.55M : 0.60M;
		}
		else
		{
			percent = Ramo == RamoEmpresa.SERVICOS ? 0.60M : 0.65M;
		}

		return decimal.Subtract(Faturamento, decimal.Multiply(percent, Faturamento));
	}
}
