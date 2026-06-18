using MealTicketManagement.Application.DTOs.MealTicket.Request;
using MealTicketManagement.Application.DTOs.MealTicket.Response;
using MealTicketManagement.Application.Interfaces.MealTicket;
using MealTicketManagement.Domain.Enums;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using MealTicketEntity = MealTicketManagement.Domain.Entities.MealTicket;
using EmployeeEntity = MealTicketManagement.Domain.Entities.Employee;

namespace MealTicketManagement.Application.UseCases.MealTicket;

public class CreateMealTicketUseCase : ICreateMealTicketUseCase
{
    private readonly IMealTicketRepository _mealTicketRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public CreateMealTicketUseCase(IMealTicketRepository mealTicketRepository, IEmployeeRepository employeeRepository)
    {
        _mealTicketRepository = mealTicketRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<MealTicketResponse> Execute(CreateMealTicketRequest request)
    {
        EmployeeEntity? employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

        if (employee is null)
            throw new BusinessException("Funcionário não encontrado.");

        if (employee.Status == Status.Inactive)
            throw new BusinessException("Não é possível criar ticket para um funcionário inativo.");

        MealTicketEntity ticket = new MealTicketEntity(employee, request.Quantity);
        await _mealTicketRepository.AddAsync(ticket);

        return new MealTicketResponse(ticket.Id, ticket.Employee.Id, ticket.Employee.Name, ticket.Quantity,
            ticket.Status, ticket.DeliveredAt);
    }
}