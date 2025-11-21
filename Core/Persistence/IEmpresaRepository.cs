using Core.Entities;

namespace Core.Persistence;

public interface IEmpresaRepository
{
	Task<Empresa> GetEmpresaByCnpj(string cnpj);
	Task<Empresa> CreateEmpresa(Empresa empresa);
}
