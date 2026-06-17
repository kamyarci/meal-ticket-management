using MealTicketManagement.Application.DTOs.Employee.Response;

namespace MealTicketManagement.Application.Interfaces;

public interface IGetAllEmployeesUseCase
{
    Task<IEnumerable<EmployeeResponse>> Execute();
}