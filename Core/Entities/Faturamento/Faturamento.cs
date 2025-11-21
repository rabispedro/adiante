namespace Core.Entities.Faturamento;

public abstract class Faturamento
{
	public decimal Value { get; init; }
	protected readonly decimal _percent;

	protected Faturamento(decimal Percent)
	{
		_percent = Percent;
	}

	public decimal GetPercent() => _percent;
}
