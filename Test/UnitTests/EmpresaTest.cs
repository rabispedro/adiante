using Empresa;

namespace Test.UnitTests;

public class EmpresaTest
{
	[Fact]
	public void GivenNullEmpresa_WhenIsValid_ThenReturnFalse()
	{
		EmpresaModel invalidEmpresa = null;

		Assert.False(Empresa.EmpresaModel.IsValid(invalidEmpresa));
	}
}
