namespace Core.Services;

public abstract class Processadora
{
	private const decimal _TAXA = 0.0465M;

	private static decimal GetPrazo(DateTime dataVencimento) =>
		(decimal)(dataVencimento.Date - DateTime.Now.Date).Days;

	private static decimal GetDesagio(decimal valorNF, decimal prazo) =>
		decimal.Divide(valorNF, (decimal)Math.Pow(decimal.ToDouble(1.0M + _TAXA), decimal.ToDouble(decimal.Divide(prazo, 30.0M))));

	public static decimal GetValorLiquido(decimal ValorNF, DateTime DataVencimento) =>
		decimal.Subtract(ValorNF, GetDesagio(ValorNF, GetPrazo(DataVencimento)));
}
