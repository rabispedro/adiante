using Empresa;
using static Empresa.EmpresaModel;

namespace Antecipacao;

public class AntecipacaoService
{
	private readonly IDictionary<CNPJ, EmpresaModel> _empresaRepository;
	private readonly IDictionary<CNPJ, CarrinhoModel> _carrinhoRepository;
	private readonly IDictionary<CNPJ, AntecipacaoModel> _antecipacaoRepository;

	public AntecipacaoService()
	{
		_empresaRepository = new SortedDictionary<CNPJ, EmpresaModel>();
		_carrinhoRepository = new SortedDictionary<CNPJ, CarrinhoModel>();
		_antecipacaoRepository = new SortedDictionary<CNPJ, AntecipacaoModel>();
	}

	public async Task<AntecipacaoModel> GetAntecipacaoByCnpjV1(CNPJ cnpj)
	{
		var empresa = _empresaRepository[cnpj] ?? throw new ArgumentException("Empresa not found");
		var today = DateOnly.FromDateTime(DateTime.Now);

		var antecipacaoExists = _antecipacaoRepository
			.Any(antecipacao => antecipacao.Key == cnpj && antecipacao.Value.Date.CompareTo(today) != 0);

		if (antecipacaoExists)
		{
			throw new ArgumentException("Antecipacao already made");
		}

		decimal percent = 0.00M, totalBruto = 0.00M, totalLiquido = 0.00M;

		if (empresa.Faturamento.Value < 10000.00M)
		{
			percent = 0.50M;
		}
		else if (empresa.Faturamento.Value < 50000.00M)
		{
			percent = empresa.Ramo == RamoEmpresa.SERVICOS ? 0.55M : 0.60M;
		}
		else
		{
			percent = empresa.Ramo == RamoEmpresa.SERVICOS ? 0.60M : 0.65M;
		}

		var notasFiscais = _carrinhoRepository
			.Where(carrinho => carrinho.Value.Cnpj.Equals(empresa.Cnpj) && carrinho.Value.DataCriacao.Month.CompareTo(DateTime.Now.Month) == 0)
			.SelectMany(notaFiscal => notaFiscal.Value.GetNotasFiscais());

		AntecipacaoModel antecipacao = new()
		{
			Cnpj = cnpj.Value,
			Empresa = empresa.Nome,
			Limite = empresa.Faturamento.Value
		};

		foreach (var notaFiscal in notasFiscais)
		{
			totalBruto = decimal.Add(totalBruto, notaFiscal.Valor);
			antecipacao.AddNotaFiscal(notaFiscal);
		}
		totalLiquido = decimal.Subtract(totalBruto, decimal.Multiply(totalBruto, percent));

		antecipacao.TotalBruto = totalBruto;
		antecipacao.TotalLiquido = totalLiquido;

		_antecipacaoRepository.Add(cnpj, antecipacao);

		return antecipacao;
	}

	// public async Task<AntecipacaoModel> GetAntecipacaoByCnpjV2(CNPJ cnpj)
	// {
	// 
	// }
}
