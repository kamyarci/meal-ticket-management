namespace MealTicketManagement.Application.DTOs.MealTicket.Request;

public record CreateMealTicketRequest(Guid EmployeeId, int Quantity);