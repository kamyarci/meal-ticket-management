using MealTicketManagement.Application.DTOs.Employee;

namespace MealTicketManagement.Application.Interfaces;

public interface ICreateEmployeeUseCase
{
    Task<EmployeeResponse> Execute(CreateEmployeeRequest request);
}