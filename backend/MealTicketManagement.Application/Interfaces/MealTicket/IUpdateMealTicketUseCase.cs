using MealTicketManagement.Application.DTOs.MealTicket.Request;
using MealTicketManagement.Application.DTOs.MealTicket.Response;

namespace MealTicketManagement.Application.Interfaces.MealTicket;

public interface IUpdateMealTicketUseCase
{
    Task<MealTicketResponse> Execute(Guid id, UpdateMealTicketRequest request);
}