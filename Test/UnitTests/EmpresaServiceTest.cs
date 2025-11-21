
using Core.Entities;

namespace Test.UnitTests;

public class EmpresaServiceTest
{
	[Fact]
	public void GivenNullEmpresa_WhenIsValid_ThenReturnFalse()
	{
		Empresa invalidEmpresa = null;

		Assert.False(Empresa.IsValid(invalidEmpresa));
	}
}
