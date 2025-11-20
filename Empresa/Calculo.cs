using Empresa.Faturamento;

namespace Empresa;

public abstract class Calculo
{
	private const decimal _taxa = 0.0465M;
	private readonly Faturamento.Faturamento _faturamento;

	public Calculo(Faturamento.Faturamento Faturamento)
	{
		_faturamento = Faturamento;
	}

	private static decimal GetPrazo(DateTime DataVencimento) => (decimal)DataVencimento.Subtract(DateTime.Today).TotalDays;
	private static decimal GetDesagio(decimal ValorNF, decimal Prazo) =>
		decimal.Divide(ValorNF, (decimal)Math.Pow(decimal.ToDouble(1.0M + _taxa), decimal.ToDouble(decimal.Divide(Prazo, 30.0M))));

	// public static decimal GetValorLiquido(decimal ValorNF, DateTime DataVencimento) =>
	// 	decimal.Multiply(_faturamento?.GetPercent() ?? 100.0m, (ValorNF - GetDesagio(ValorNF, GetPrazo(DataVencimento))));
}
