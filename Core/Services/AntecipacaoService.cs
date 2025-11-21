using System.Text.Json;
using Core.Entities;
using Core.Persistence;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;

namespace Core.Services;

public class AntecipacaoService
{
	private readonly IAntecipacaoRepository _antecipacaoRepository;
	private readonly IDistributedCache _cache;
	private readonly IEmpresaRepository _empresaRepository;
	private readonly INotaFiscalRepository _notaFiscalRepository;

	public AntecipacaoService(
		IAntecipacaoRepository antecipacaoRepository,
		IDistributedCache cache,
		IEmpresaRepository empresaRepository,
		INotaFiscalRepository notaFiscalRepository)
	{
		_antecipacaoRepository = antecipacaoRepository;
		_cache = cache;
		_empresaRepository = empresaRepository;
		_notaFiscalRepository = notaFiscalRepository;
	}

	public async Task<Antecipacao> GetAntecipacaoByCnpjV1(CNPJ cnpj)
	{
		var empresa = await _empresaRepository.GetEmpresaByCnpj(cnpj.Value);

		if (empresa == null)
		{
			throw new ArgumentException("Empresa not found");
		}

		var today = DateTime.Today;

		var antecipacaoExists = await _antecipacaoRepository.ExistsAntecipacaoByCnpjWithinMonth(cnpj.Value, today);

		if (antecipacaoExists)
		{
			throw new ArgumentException("Antecipacao already made");
		}

		decimal percent = 0.00M, totalBruto = 0.00M, totalLiquido = 0.00M;

		if (empresa.Faturamento < 10000.00M)
		{
			percent = 0.00M;
		}
		else if (empresa.Faturamento < 50000.00M)
		{
			percent = 0.50M;
		}
		else if (empresa.Faturamento < 100000.00M)
		{
			percent = empresa.Ramo == Empresa.RamoEmpresa.SERVICOS ? 0.55M : 0.60M;
		}
		else
		{
			percent = empresa.Ramo == Empresa.RamoEmpresa.SERVICOS ? 0.60M : 0.65M;
		}

		var carrinhoCached = await _cache.GetAsync($"carrinho:{cnpj.Value}:${today}");

		if (carrinhoCached == null)
		{
			throw new ArgumentException("Antecipacao sem Carrinho");
		}

		var carrinho = JsonSerializer.Deserialize<Carrinho>(carrinhoCached);

		var notasFiscais = carrinho.GetNotasFiscais();

		Antecipacao antecipacao = new()
		{
			Cnpj = cnpj.Value,
			Empresa = empresa.Nome,
			Limite = empresa.Faturamento
		};

		foreach (var notaFiscal in notasFiscais)
		{
			var valorLiquido = Processadora.GetValorLiquido(notaFiscal.Valor, notaFiscal.DataVencimento);

			totalBruto = decimal.Add(totalBruto, notaFiscal.Valor);
			totalLiquido = decimal.Add(totalLiquido, valorLiquido);

			NotaFiscalCheckout notaFiscalCheckout = new()
			{
				Numero = notaFiscal.Numero,
				ValorBruto = notaFiscal.Valor,
				ValorLiquido = valorLiquido
			};

			antecipacao.NotasFiscais.Add(notaFiscalCheckout);
		}

		antecipacao.TotalBruto = totalBruto;
		antecipacao.TotalLiquido = totalLiquido;

		await _antecipacaoRepository.CreateAntecipacao(antecipacao);
		await _cache.RemoveAsync($"carrinho:{cnpj.Value}:${today}");

		return antecipacao;
	}

	public async Task<Antecipacao> GetAntecipacaoByCnpjV2(CNPJ cnpj)
	{
		var empresa = await _empresaRepository.GetEmpresaByCnpj(cnpj.Value);

		if (empresa == null)
		{
			throw new ArgumentException("Empresa not found");
		}

		var today = DateTime.Today;

		var antecipacaoExists = await _antecipacaoRepository.ExistsAntecipacaoByCnpjWithinMonth(cnpj.Value, today);

		if (antecipacaoExists)
		{
			throw new ArgumentException("Antecipacao already made");
		}

		var carrinhoCached = await _cache.GetAsync($"carrinho:{cnpj.Value}:${today}");

		var carrinho = JsonSerializer.Deserialize<Carrinho>(carrinhoCached);

		var notasFiscais = carrinho.GetNotasFiscais();



		return new Antecipacao();
	}

	public async Task<decimal> GetLimiteAntecipacao(CNPJ cnpj)
	{
		var empresa = await _empresaRepository.GetEmpresaByCnpj(cnpj.Value);

		if (empresa == null)
		{
			throw new ArgumentException("Empresa not found");
		}

		decimal percent = 0.00M;

		if (empresa.Faturamento < 10000.00M)
		{
			percent = 0.00M;
		}
		else if (empresa.Faturamento < 50000.00M)
		{
			percent = 0.50M;
		}
		else if (empresa.Faturamento < 100000.00M)
		{
			percent = empresa.Ramo == Empresa.RamoEmpresa.SERVICOS ? 0.55M : 0.60M;
		}
		else
		{
			percent = empresa.Ramo == Empresa.RamoEmpresa.SERVICOS ? 0.60M : 0.65M;
		}

		return decimal.Subtract(empresa.Faturamento, decimal.Multiply(percent, empresa.Faturamento));
	}

	public async Task<Carrinho> AddNotaFiscalToCart(CNPJ cnpj, long numeroNotaFiscal)
	{
		var today = DateTime.Today;


		var carrinhoCached = await _cache.GetAsync($"carrinho:{cnpj.Value}:${today}");

		var carrinho = carrinhoCached == null ? new Carrinho() : JsonSerializer.Deserialize<Carrinho>(carrinhoCached);

		var notaFiscal = await _notaFiscalRepository.GetNotaFiscalByNumero(numeroNotaFiscal);

		if (notaFiscal == null)
		{
			throw new ArgumentException("Nota Fiscal not found");
		}

		var empresa = await _empresaRepository.GetEmpresaByCnpj(cnpj.Value);

		if (empresa == null)
		{
			throw new ArgumentException("Empresa not found");
		}

		var valorCarrinho = carrinho
			.GetNotasFiscais()
			.Sum(notaFiscal => notaFiscal.Valor);

		if (empresa.Faturamento < valorCarrinho)
		{
			throw new ArgumentException("Valor do carrinho maior que faturamento");
		}

		carrinho.AddNotaFiscal(notaFiscal);

		await _cache.SetAsync($"carrinho:{cnpj.Value}:${today}", JsonSerializer.SerializeToUtf8Bytes(carrinho), new DistributedCacheEntryOptions() { AbsoluteExpiration = DateTime.Now.AddDays(2)});

		return carrinho;
	}

	public async Task<Carrinho> RemoveNotaFiscalFromCart(CNPJ cnpj, long numeroNotaFiscal)
	{
		var today = DateTime.Today;

		var carrinhoCached = await _cache.GetAsync($"carrinho:{cnpj.Value}:${today}");

		var carrinho = JsonSerializer.Deserialize<Carrinho>(carrinhoCached);
		if (carrinho == null)
		{
			throw new ArgumentException("Carrinho not found");
		}

		var notaFiscal = carrinho
			.GetNotasFiscais()
			.First(notaFiscal => notaFiscal.Numero == numeroNotaFiscal);

		if (notaFiscal == null)
		{
			throw new ArgumentException("Nota Fiscal not found");
		}

		carrinho.RemoveNotaFiscal(notaFiscal);
		await _cache.RefreshAsync($"carrinho:{cnpj.Value}:${today}");

		return carrinho;
	}
}
