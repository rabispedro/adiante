namespace Core.Services;

public abstract class Processadora
{
	private const decimal _TAXA = 0.0465M;

	private static decimal GetPrazo(DateTime dataVencimento) =>
		(decimal)dataVencimento.Subtract(DateTime.Today).TotalDays;

	private static decimal GetDesagio(decimal ValorNF, decimal Prazo) =>
		decimal.Divide(ValorNF, (decimal)Math.Pow(decimal.ToDouble(1.0M + _TAXA), decimal.ToDouble(decimal.Divide(Prazo, 30.0M))));

	public static decimal GetValorLiquido(decimal ValorNF, DateTime DataVencimento) =>
		ValorNF - GetDesagio(ValorNF, GetPrazo(DataVencimento));
}
