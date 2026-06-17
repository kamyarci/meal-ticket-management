using MealTicketManagement.Application.DTOs.Employee.Response;
using MealTicketManagement.Application.Interfaces;
using MealTicketManagement.Domain.Interfaces;

namespace MealTicketManagement.Application.UseCases.Employee;

public class GetAllEmployeesUseCase : IGetAllEmployeesUseCase
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetAllEmployeesUseCase(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<EmployeeResponse>> Execute()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return employees.Select(e => new EmployeeResponse(e.Id, e.Name, e.Cpf, e.Status, e.UpdatedAt));
    }
}