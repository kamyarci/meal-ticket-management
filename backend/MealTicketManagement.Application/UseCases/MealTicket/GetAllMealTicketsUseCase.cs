using MealTicketManagement.Application.DTOs.MealTicket.Response;
using MealTicketManagement.Application.Interfaces.MealTicket;
using MealTicketManagement.Domain.Interfaces;
using MealTicketEntity = MealTicketManagement.Domain.Entities.MealTicket;

namespace MealTicketManagement.Application.UseCases.MealTicket;

public class GetAllMealTicketsUseCase : IGetAllMealTicketsUseCase
{
    private readonly IMealTicketRepository _mealTicketRepository;

    public GetAllMealTicketsUseCase(IMealTicketRepository mealTicketRepository)
    {
        _mealTicketRepository = mealTicketRepository;
    }

    public async Task<IEnumerable<MealTicketResponse>> Execute()
    {
        IEnumerable<MealTicketEntity> tickets = await _mealTicketRepository.GetAllAsync();

        return tickets.Select(t => new MealTicketResponse(t.Id, t.Employee.Id, t.Employee.Name,
            t.Quantity, t.Status, t.DeliveredAt));
    }
}
