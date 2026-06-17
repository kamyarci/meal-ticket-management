using MealTicketManagement.Application.DTOs.Employee.Response;

namespace MealTicketManagement.Application.Interfaces;

public interface IGetEmployeeByIdUseCase
{
    Task<EmployeeResponse> Execute(Guid id);
}