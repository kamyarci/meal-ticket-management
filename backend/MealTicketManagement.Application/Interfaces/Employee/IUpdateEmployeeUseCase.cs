using MealTicketManagement.Application.DTOs.Employee.Request;
using MealTicketManagement.Application.DTOs.Employee.Response;

namespace MealTicketManagement.Application.Interfaces.Employee;

public interface IUpdateEmployeeUseCase
{
    Task<EmployeeResponse> Execute(Guid id, UpdateEmployeeRequest request);
}