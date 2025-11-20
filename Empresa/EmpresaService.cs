using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Empresa;

public class EmpresaService
{
	private readonly IDictionary<CNPJ, EmpresaModel> _empresaRepository;
	private readonly IDictionary<long, NotaFiscalModel> _notaFiscalRepository;
	private readonly IDictionary<CNPJ, CarrinhoModel> _carrinhoRepository;

	public EmpresaService()
	{
		_empresaRepository = new SortedDictionary<CNPJ, EmpresaModel>();
		_notaFiscalRepository = new SortedDictionary<long, NotaFiscalModel>();
		_carrinhoRepository = new SortedDictionary<CNPJ, CarrinhoModel>();
	}

	public async Task<EmpresaModel> GetEmpresaByCnpj(CNPJ cnpj)
	{
		var empresa = _empresaRepository[cnpj];
		return empresa ?? throw new ArgumentException("Empresa not found");
	}

	public async Task<EmpresaModel> CreateEmpresa(EmpresaModel empresa)
	{
		if (EmpresaModel.IsValid(empresa))
		{
			throw new ArgumentException("Invalid Empresa");
		}

		_empresaRepository.Add(empresa.Cnpj, empresa);

		return empresa;
	}

	public async Task<NotaFiscalModel> CreateNotaFiscal(EmpresaModel empresa, NotaFiscalModel notaFiscal)
	{
		if (!NotaFiscalModel.IsValid(notaFiscal))
		{
			throw new ArgumentException("Invalid Nota Fiscal");
		}

		var empresaFounded = _empresaRepository[empresa.Cnpj] ?? throw new ArgumentException("Empresa not found");

		notaFiscal.Empresa = empresaFounded;

		_notaFiscalRepository.Add(notaFiscal.Numero, notaFiscal);

		return notaFiscal;
	}

	public async Task<CarrinhoModel> AddNotaFiscalToCart(CNPJ cnpj, long numeroNotaFiscal)
	{
		var carrinho = _carrinhoRepository[cnpj] ?? new CarrinhoModel();

		var notaFiscal = _notaFiscalRepository[numeroNotaFiscal] ?? throw new ArgumentException("Nota Fiscal not found");

		carrinho.AddNotaFiscal(notaFiscal);

		return carrinho;
	}

	public async Task<CarrinhoModel> RemoveNotaFiscalFromCart(CNPJ cnpj, long numeroNotaFiscal)
	{
		var carrinho = _carrinhoRepository[cnpj] ?? throw new ArgumentException("Carrinho not found");

		var notaFiscal = _notaFiscalRepository[numeroNotaFiscal] ?? throw new ArgumentException("Nota Fiscal not found");

		carrinho.RemoveNotaFiscal(notaFiscal);

		return carrinho;
	}
}
