namespace MealTicketManagement.Application.DTOs.MealTicket;

public record CreateMealTicketRequest(Guid EmployeeId, int Quantity);