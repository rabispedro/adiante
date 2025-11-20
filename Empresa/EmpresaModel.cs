using System.Text.Json.Serialization;
using Empresa;
using Empresa.Faturamento;

namespace Empresa;

public class EmpresaModel
{
	[JsonPropertyName("cnpj")]
	public CNPJ Cnpj { get; init; }
	
	[JsonPropertyName("nome")]
	public string Nome { get; init; } = string.Empty;
	
	[JsonPropertyName("faturamento")]
	public Faturamento.Faturamento Faturamento { get; init; }

	[JsonPropertyName("ramo")]
	public RamoEmpresa Ramo { get; init; }

	public enum RamoEmpresa
	{
		SERVICOS,
		PRODUTOS
	}

	public static bool IsValid(EmpresaModel empresa)
	{
		return  (empresa != null) && !string.IsNullOrWhiteSpace(empresa.Nome) && CNPJ.IsValid(empresa.Cnpj.Value);
	}
}
