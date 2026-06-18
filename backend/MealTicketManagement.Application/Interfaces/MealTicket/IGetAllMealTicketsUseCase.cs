using MealTicketManagement.Application.DTOs.MealTicket.Response;

namespace MealTicketManagement.Application.Interfaces.MealTicket;

public interface IGetAllMealTicketsUseCase
{
    Task<IEnumerable<MealTicketResponse>> Execute();
}
