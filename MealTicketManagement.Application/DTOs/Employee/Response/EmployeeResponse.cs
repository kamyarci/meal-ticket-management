using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Application.DTOs.Employee.Response;

public record EmployeeResponse(Guid Id, string Name, string Cpf, Status Status, DateTime UpdatedAt);