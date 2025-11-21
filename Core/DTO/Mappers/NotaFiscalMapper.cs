using Core.Entities;

namespace Core.DTO.Mappers;

public static class NotaFiscalMapper
{
	public static NotaFiscal CreateNotaFiscalDTOToNotaFiscal(CreateNotaFiscalDTO notaFiscalDTO)
	{
		return new()
		{
			DataVencimento = notaFiscalDTO.DataVencimento,
			Numero = notaFiscalDTO.Numero,
			Valor = notaFiscalDTO.Valor
		};
	}

}
