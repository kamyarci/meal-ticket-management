using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Application.DTOs.MealTicket;

public record MealTicketResponse(
    Guid Id,
    Guid EmployeeId,
    string EmployeeName,
    int Quantity,
    Status Status,
    DateTime DeliveredAt);