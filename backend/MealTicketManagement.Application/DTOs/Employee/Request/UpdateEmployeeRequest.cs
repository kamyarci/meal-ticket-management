using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Application.DTOs.Employee.Request;

public record UpdateEmployeeRequest(string Name, Status Status);