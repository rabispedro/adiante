using Empresa;

namespace Test.UnitTest;

public class CNPJTest
{
	[Fact]
	public void GivenInvalidCnpjNumber_WhenIsValid_ThenReturnFalse()
	{
		string invalidCnpj = "03278096000130";
		bool result = CNPJ.IsValid(invalidCnpj);
		Assert.False(result);
	}

	[Fact]
	public void GivenInvalidCnpjWithLetters_WhenIsValid_ThenReturnFalse()
	{
		string invalidCnpj = "03278o96ooo142";
		bool result = CNPJ.IsValid(invalidCnpj);
		Assert.False(result);
	}

	[Fact]
	public void GivenNullCnpjNumber_WhenIsValid_ThenReturnFalse()
	{
		string invalidCnpj = null;
		bool result = CNPJ.IsValid(invalidCnpj);
		Assert.False(result);
	}

	[Fact]
	public void GivenEmptyCnpjNumber_WhenIsValid_ThenReturnFalse()
	{
		string invalidCnpj = "   ";
		bool result = CNPJ.IsValid(invalidCnpj);
		Assert.False(result);
	}

	[Fact]
	public void GivenValidCnpjNumber_WhenIsValid_ThenReturnTrue()
	{
		string validCnpj = "03278096000142";
		bool result = CNPJ.IsValid(validCnpj);
		Assert.True(result);
	}

	[Fact]
	public void GivenInvalidCnpj_WhenConstruct_ThenThrowException()
	{
		string invalidCnpj = "03278o96ooo142";
		Assert.Throws<ArgumentException>(() => new CNPJ(invalidCnpj));
    }

    [Fact]
    public void GivenValidCnpj_WhenConstruct_ThenConstructNewCnpj()
    {
        string validCnpj = "61145559000102";
        Assert.IsType<CNPJ>(new CNPJ(validCnpj));
    }
}
