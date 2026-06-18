using MealTicketManagement.Application.DTOs.MealTicket.Request;
using MealTicketManagement.Application.DTOs.MealTicket.Response;
using MealTicketManagement.Application.Interfaces.MealTicket;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using MealTicketEntity = MealTicketManagement.Domain.Entities.MealTicket;

namespace MealTicketManagement.Application.UseCases.MealTicket;

public class UpdateMealTicketUseCase: IUpdateMealTicketUseCase
{
    private readonly IMealTicketRepository _mealTicketRepository;

    public UpdateMealTicketUseCase(IMealTicketRepository mealTicketRepository)
    {
        _mealTicketRepository = mealTicketRepository;
    }

    public async Task<MealTicketResponse> Execute(Guid id, UpdateMealTicketRequest request)
    {
        MealTicketEntity? ticketMeal = await _mealTicketRepository.GetByIdAsync(id);
        if (ticketMeal is null)
            throw new BusinessException("Ticket não encontrado.");

        ticketMeal.Update(request.Quantity, request.Status);
        await _mealTicketRepository.UpdateAsync(ticketMeal);

        return new MealTicketResponse(ticketMeal.Id, ticketMeal.Employee.Id, ticketMeal.Employee.Name,
            ticketMeal.Quantity, ticketMeal.Status, ticketMeal.DeliveredAt);
    }
}