namespace Core.Services;

public abstract class Processadora
{
	private const decimal _TAXA = 0.0465M;

	private static decimal GetPrazo(DateTime dataVencimento) =>
		(decimal)(dataVencimento.Date - DateTime.Today.Date).Days;

	private static decimal GetTaxa(decimal prazo) =>
		Math.Ceiling(decimal.Multiply(decimal.Divide(prazo, 30.0M), _TAXA));

	private static decimal GetDesagio(decimal valorNF, decimal prazo) =>
		decimal.Divide(valorNF, (decimal)Math.Pow(decimal.ToDouble(1.0M + GetTaxa(prazo)), decimal.ToDouble(decimal.Divide(prazo, 30.0M))));

	public static decimal GetValorLiquido(decimal valorNF, DateTime dataVencimento) =>
		decimal.Subtract(valorNF, GetDesagio(valorNF, GetPrazo(dataVencimento)));
}
