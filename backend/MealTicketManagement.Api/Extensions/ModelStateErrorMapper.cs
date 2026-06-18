using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MealTicketManagement.Api.Extensions;

internal static class ModelStateErrorMapper
{
    internal static string Map(string key, ModelError error) => (key, error) switch
    {
        _ when error.ErrorMessage.Contains("could not be converted") && key.Contains("status")
            => "Status inválido. Use Active ou Inactive.",
        _ when error.ErrorMessage.Contains("required")
            => $"O campo {key} é obrigatório.",
        _ when error.ErrorMessage.Contains("could not be converted") ||
               error.ErrorMessage.Contains("is an invalid start of a value") ||
               error.ErrorMessage.Contains("is not valid")
            => $"Valor inválido para o campo {key.Replace("$.", "")}.",
        _ => error.ErrorMessage
    };
}