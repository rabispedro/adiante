using Core.DTO;
using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : ControllerBase
{
	private readonly EmpresaService _empresaService;

	public EmpresaController(EmpresaService empresaService)
	{
		_empresaService = empresaService;
	}

	[HttpGet("{cnpj}")]
	public async Task<IActionResult> GetEmpresaByCnpj([FromRoute] string cnpj) =>
		Ok(await _empresaService.GetEmpresaByCnpj(new CNPJ(cnpj)));

	[HttpPost]
	public async Task<IActionResult> CreateEmpresa([FromBody] CreateEmpresaDTO empresaDto) =>
		Ok(await _empresaService.CreateEmpresa(empresaDto));

	[HttpPost("{cnpj}/nota-fiscal")]
	public async Task<IActionResult> CreateNotaFiscal([FromRoute] string cnpj, [FromBody] CreateNotaFiscalDTO notaFiscalDto) =>
		Ok(await _empresaService.CreateNotaFiscal(new CNPJ(cnpj), notaFiscalDto));
}
