using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Application.DTOs.MealTicket;

public record UpdateMealTicketRequest(int Quantity, Status Status);