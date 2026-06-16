using MealTicketManagement.Application.DTOs.Employee;

namespace MealTicketManagement.Application.Interfaces;

public interface IGetEmployeeByIdUseCase
{
    Task<EmployeeResponse> Execute(Guid id);
}