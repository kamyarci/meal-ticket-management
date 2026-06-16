using MealTicketManagement.Application.DTOs.Employee;

namespace MealTicketManagement.Application.Interfaces;

public interface IUpdateEmployeeUseCase
{
    Task<EmployeeResponse> Execute(Guid id, UpdateEmployeeRequest request);
}