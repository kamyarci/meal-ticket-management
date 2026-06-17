using MealTicketManagement.Application.DTOs.Employee.Request;
using MealTicketManagement.Application.DTOs.Employee.Response;

namespace MealTicketManagement.Application.Interfaces;

public interface ICreateEmployeeUseCase
{
    Task<EmployeeResponse> Execute(CreateEmployeeRequest request);
}