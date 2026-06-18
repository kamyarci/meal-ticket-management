using MealTicketManagement.Domain.Exceptions;

namespace MealTicketManagement.Domain.Guards;

public static class Guard
{
    public static void IsNotNull(object? value, string message)
    {
        if (value is null)
            throw new BusinessException(message);
    }

    public static void IsNotNullOrEmpty(string value, string message)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new BusinessException(message);
    }

    public static void IsGreaterThan<T>(T value, T compareTo, string message) where T : IComparable<T>
    {
        if (value.CompareTo(compareTo) <= 0)
            throw new BusinessException(message);
    }

    public static void IsFalse(bool condition, string message)
    {
        if (condition)
            throw new BusinessException(message);
    }

    public static void IsCpfValid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            throw new BusinessException("CPF não pode ser vazio.");

        cpf = cpf.Trim().Replace(".", "").Replace("-", "");

        if (!cpf.All(char.IsDigit))
            throw new BusinessException("CPF deve conter apenas números.");

        if (cpf.Length != 11)
            throw new BusinessException("CPF deve ter 11 dígitos.");

        if (cpf.All(c => c == cpf[0]))
            throw new BusinessException("CPF inválido.");

        int[] pesosPrimeiroDigito = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] pesosSegundoDigito = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int primeiroDigito = CalcularDigitoCpf(cpf[..9], pesosPrimeiroDigito);
        int segundoDigito = CalcularDigitoCpf(cpf[..9] + primeiroDigito, pesosSegundoDigito);

        if (primeiroDigito != cpf[9] - '0' || segundoDigito != cpf[10] - '0')
            throw new BusinessException("CPF inválido.");
    }

    private static int CalcularDigitoCpf(string cpf, int[] pesos)
    {
        int soma = 0;
        for (int i = 0; i < pesos.Length; i++)
            soma += (cpf[i] - '0') * pesos[i];

        int resto = soma % 11;
        return resto < 2 ? 0 : 11 - resto;
    }
}