using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Application.DTOs.MealTicket.Request;

public record UpdateMealTicketRequest(int Quantity, Status Status);