using MealTicketManagement.Application.DTOs.MealTicket.Response;
using MealTicketManagement.Application.Interfaces.MealTicket;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using MealTicketEntity = MealTicketManagement.Domain.Entities.MealTicket;

namespace MealTicketManagement.Application.UseCases.MealTicket;

public class GetMealTicketByIdUseCase : IGetMealTicketByIdUseCase
{
    private readonly IMealTicketRepository _mealTicketRepository;

    public GetMealTicketByIdUseCase(IMealTicketRepository mealTicketRepository)
    {
        _mealTicketRepository = mealTicketRepository;
    }

    public async Task<MealTicketResponse> Execute(Guid id)
    {
        MealTicketEntity? ticketMeal = await _mealTicketRepository.GetByIdAsync(id);
        if (ticketMeal is null)
            throw new BusinessException("Ticket não encontrado.");

        return new MealTicketResponse(ticketMeal.Id, ticketMeal.Employee.Id, ticketMeal.Employee.Name,
            ticketMeal.Quantity, ticketMeal.Status, ticketMeal.DeliveredAt);
    }
}