using Core.Entities;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AntecipacaoController : ControllerBase
{
	private readonly AntecipacaoService _antecipacaoService;

	public AntecipacaoController(AntecipacaoService antecipacaoService)
	{
		_antecipacaoService = antecipacaoService;
	}

	[HttpGet("{cnpj}/v1")]
	public async Task<IActionResult> GetAntecipacaoByCnpj([FromRoute] string cnpj) =>
		Ok(await _antecipacaoService.GetAntecipacaoByCnpjV1(new CNPJ(cnpj)));

	[HttpGet("{cnpj}/limite")]
	public async Task<IActionResult> GetLimiteAntecipacao([FromRoute] string cnpj) =>
		Ok(await _antecipacaoService.GetLimiteAntecipacao(new CNPJ(cnpj)));

	[HttpPost("{cnpj}/carrinho/{numeroNotaFiscal}")]
	public async Task<IActionResult> AddNotaFiscalToCart([FromRoute] string cnpj, [FromRoute] long numeroNotaFiscal) =>
		Ok(await _antecipacaoService.AddNotaFiscalToCart(new CNPJ(cnpj), numeroNotaFiscal));

	[HttpDelete("{cnpj}/carrinho/{numeroNotaFiscal}")]
	public async Task<IActionResult> RemoveNotaFiscalFromCart([FromRoute] string cnpj, [FromRoute] long numeroNotaFiscal) =>
		Ok(await _antecipacaoService.RemoveNotaFiscalFromCart(new CNPJ(cnpj), numeroNotaFiscal));
}
