using MealTicketManagement.Application.DTOs.Employee.Response;

namespace MealTicketManagement.Application.Interfaces.Employee;

public interface IGetEmployeeByIdUseCase
{
    Task<EmployeeResponse> Execute(Guid id);
}