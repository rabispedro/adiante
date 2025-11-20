using Microsoft.AspNetCore.Mvc;

namespace Empresa;


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
	public async Task<IActionResult> GetEmpresaById([FromRoute] CNPJ cnpj) =>
		Ok(await _empresaService.GetEmpresaByCnpj(cnpj));

	[HttpPost]
	public async Task<IActionResult> CreateEmpresa([FromBody] EmpresaModel empresa) =>
		Ok(await _empresaService.CreateEmpresa(empresa));

	[HttpPost("{cnpj}/nota-fiscal")]
	public async Task<IActionResult> CreateNotaFiscal([FromBody] EmpresaModel empresa, [FromForm] NotaFiscalModel notaFiscal) =>
		Ok(await _empresaService.CreateNotaFiscal(empresa, notaFiscal));

	[HttpPost("{cnpj}/carrinho/{numeroNotaFiscal}")]
	public async Task<IActionResult> AddNotaFiscalToCart([FromRoute] string cnpj, [FromRoute] long numeroNotaFiscal) =>
		Ok(await _empresaService.AddNotaFiscalToCart(new CNPJ(cnpj), numeroNotaFiscal));

	[HttpDelete("{cnpj}/carrinho/{numeroNotaFiscal}")]
	public async Task<IActionResult> RemoveNotaFiscalFromCart([FromRoute] string cnpj, [FromRoute] long numeroNotaFiscal) =>
		Ok(await _empresaService.RemoveNotaFiscalFromCart(new CNPJ(cnpj), numeroNotaFiscal));
}
