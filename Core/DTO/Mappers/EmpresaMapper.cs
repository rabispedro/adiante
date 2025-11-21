using Core.Entities;

namespace Core.DTO.Mappers;

public static class EmpresaMapper
{
	public static Empresa CreateEmpresaDTOToEmpresa(CreateEmpresaDTO empresaDTO)
	{
		return new()
		{
			Cnpj = new CNPJ(empresaDTO.Cnpj).Value,
			Faturamento = empresaDTO.Faturamento,
			Nome = empresaDTO.Nome,
			Ramo = (Empresa.RamoEmpresa)empresaDTO.Ramo
		};
	}
}
