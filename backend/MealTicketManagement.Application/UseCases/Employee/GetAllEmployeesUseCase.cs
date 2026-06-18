using MealTicketManagement.Application.DTOs.Employee.Response;
using MealTicketManagement.Application.Interfaces.Employee;
using MealTicketManagement.Domain.Interfaces;
using EmployeeEntity = MealTicketManagement.Domain.Entities.Employee;

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
        IEnumerable<EmployeeEntity> employees = await _employeeRepository.GetAllAsync();
        return employees.Select(e => new EmployeeResponse(e.Id, e.Name, e.Cpf, e.Status, e.UpdatedAt));
    }
}