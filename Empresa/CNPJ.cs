using System.Text.RegularExpressions;

namespace Empresa;

public class CNPJ
{
	public string Value { get; }

	public CNPJ(string cnpj)
	{
		if (!IsValid(cnpj))
		{
			throw new ArgumentException("Invalid CNPJ");
		}
		Value = cnpj;
	}

	public static bool IsValid(string cnpj)
	{
		// NOTE using this resource https://regexr.com/ to validate only digits in the string
		if (string.IsNullOrWhiteSpace(cnpj) || cnpj.Length != 14 || !Regex.IsMatch(cnpj, "^[\\d]{14}(?!\\w)", RegexOptions.CultureInvariant))
		{
			return false;
		}

		// NOTE using this resource https://www.macoratti.net/alg_cnpj.htm to validate cnpj numbers
		int[] numbers = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
		int[] validationDigits = [0, 0];
		int sum = 0;

		for (int i = 0; i < 12; i++)
		{
			int digit = (cnpj[i] - '0') * numbers[i];
			sum += digit;
		}
		int module = sum % 11;
		validationDigits[0] = (module > 2) ? 11 - module : 0;

		numbers = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
		sum = 0;

		for (int i = 0; i < 13; i++)
		{
			int digit = (cnpj[i] - '0') * numbers[i];
			sum += digit;
		}
		module = sum % 11;
		validationDigits[1] = (module > 2) ? 11 - module : 0;

		return ((cnpj[12] - '0') == validationDigits[0] && (cnpj[13] - '0') == validationDigits[1]);
	}
}
