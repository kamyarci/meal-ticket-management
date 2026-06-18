using MealTicketManagement.Application.DTOs.MealTicket.Response;

namespace MealTicketManagement.Application.Interfaces.MealTicket;

public interface IGetMealTicketByIdUseCase
{
    Task<MealTicketResponse> Execute(Guid id);
}