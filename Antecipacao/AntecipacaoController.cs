using Empresa;
using Microsoft.AspNetCore.Mvc;

namespace Antecipacao;

[ApiController]
[Route("api/[controller]")]
public class AntecipacaoController : ControllerBase
{
	private readonly AntecipacaoService _antecipacaoService;

	public AntecipacaoController(AntecipacaoService antecipacaoService)
	{
		_antecipacaoService = antecipacaoService;
	}

	[HttpGet("{cnpj}")]
	public async Task<IActionResult> GetAntecipacaoByCnpj([FromRoute] string cnpj) =>
		Ok(await _antecipacaoService.GetAntecipacaoByCnpjV1(new CNPJ(cnpj)));
}
