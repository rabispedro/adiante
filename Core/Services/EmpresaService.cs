using Core.DTO;
using Core.DTO.Mappers;
using Core.Entities;
using Core.Persistence;

namespace Core.Services;

public class EmpresaService
{
	private readonly IEmpresaRepository _empresaRepository;
	private readonly INotaFiscalRepository _notaFiscalRepository;

	public EmpresaService(IEmpresaRepository empresaRepository, INotaFiscalRepository notaFiscalRepository)
	{
		_empresaRepository = empresaRepository;
		_notaFiscalRepository = notaFiscalRepository;
	}

	public async Task<Empresa> GetEmpresaByCnpj(CNPJ cnpj)
	{
		var empresa = await _empresaRepository.GetEmpresaByCnpj(cnpj.Value);

		return empresa ?? throw new ArgumentException("Empresa not found");
	}

	public async Task<Empresa> CreateEmpresa(CreateEmpresaDTO empresaDto)
	{
		Console.WriteLine(empresaDto);

		var empresa = EmpresaMapper.CreateEmpresaDTOToEmpresa(empresaDto);

		if (!Empresa.IsValid(empresa))
		{
			throw new ArgumentException("Invalid Empresa");
		}

		await _empresaRepository.CreateEmpresa(empresa);

		return empresa;
	}

	public async Task<NotaFiscal> CreateNotaFiscal(CNPJ cnpj, CreateNotaFiscalDTO notaFiscalDto)
	{
		var notaFiscal = NotaFiscalMapper.CreateNotaFiscalDTOToNotaFiscal(notaFiscalDto);
		
		if (!NotaFiscal.IsValid(notaFiscal))
		{
			throw new ArgumentException("Invalid Nota Fiscal");
		}

		var empresa = await _empresaRepository.GetEmpresaByCnpj(cnpj.Value);
		
		if (empresa == null)
		{
			throw new ArgumentException("Empresa not found");
		}

		notaFiscal.Empresa = empresa;

		await _notaFiscalRepository.CreateNotaFiscal(notaFiscal);

		return notaFiscal;
	}
}
