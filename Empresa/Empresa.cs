namespace Empresa;

public class Empresa
{
	public CNPJ Cnpj { get; init; }
	public string Nome { get; set; } = string.Empty;
	public decimal Faturamento { get; set; }
	public Ramo Ramo { get; set; }
}
