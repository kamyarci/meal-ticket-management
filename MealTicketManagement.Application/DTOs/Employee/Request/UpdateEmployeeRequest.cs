using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Application.DTOs.Employee;

public record UpdateEmployeeRequest(string Name, Status Status);