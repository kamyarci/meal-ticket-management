using MealTicketManagement.Application.DTOs.Employee.Response;
using MealTicketManagement.Application.Interfaces;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;

namespace MealTicketManagement.Application.UseCases.Employee;

public class GetEmployeeByIdUseCase : IGetEmployeeByIdUseCase
{
    private readonly IEmployeeRepository _employeeRepository;

    public GetEmployeeByIdUseCase(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeResponse> Execute(Guid id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee is null)
            throw new BusinessException("Funcionário não encontrado.");

        return new EmployeeResponse(employee.Id, employee.Name, employee.Cpf, employee.Status, employee.UpdatedAt);
    }
}