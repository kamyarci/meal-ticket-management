using MealTicketManagement.Application.DTOs.Employee.Response;

namespace MealTicketManagement.Application.Interfaces.Employee;

public interface IGetAllEmployeesUseCase
{
    Task<IEnumerable<EmployeeResponse>> Execute();
}