using MealTicketManagement.Application.DTOs.MealTicket.Request;
using MealTicketManagement.Application.DTOs.MealTicket.Response;

namespace MealTicketManagement.Application.Interfaces.MealTicket;

public interface ICreateMealTicketUseCase
{
    Task<MealTicketResponse> Execute(CreateMealTicketRequest request);
}