using MealTicketManagement.Application.DTOs.Employee;

namespace MealTicketManagement.Application.Interfaces;

public interface IGetAllEmployeesUseCase
{
    Task<IEnumerable<EmployeeResponse>> Execute();
}