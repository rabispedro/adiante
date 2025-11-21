using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Empresa
{
	[Key]
	public string Cnpj { get; init; }
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

	// [JsonConstructor]
	// public Empresa(string cnpj, string nome, decimal faturamento, string ramo)
	// {
	// 	Cnpj = new CNPJ(cnpj);
	// 	Nome = nome;
	// 	Faturamento = faturamento;

	// 	if (ramo.ToLowerInvariant().Equals(RamoEmpresa.PRODUTOS.ToString().ToLowerInvariant()))
	// 	{
	// 		Ramo = RamoEmpresa.PRODUTOS;
	// 	}
	// 	else if (ramo.ToLowerInvariant().Equals(RamoEmpresa.SERVICOS.ToString().ToLowerInvariant()))
	// 	{
	// 		Ramo = RamoEmpresa.SERVICOS;
	// 	}
	// 	else
	// 	{
	// 		throw new ArgumentException("Invalid Ramo");
	// 	}
	// }
}
