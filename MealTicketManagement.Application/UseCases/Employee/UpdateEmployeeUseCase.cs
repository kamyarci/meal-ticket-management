using MealTicketManagement.Application.DTOs.Employee.Request;
using MealTicketManagement.Application.DTOs.Employee.Response;
using MealTicketManagement.Application.Interfaces.Employee;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;

namespace MealTicketManagement.Application.UseCases.Employee;

public class UpdateEmployeeUseCase : IUpdateEmployeeUseCase
{
    private readonly IEmployeeRepository _employeeRepository;

    public UpdateEmployeeUseCase(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeResponse> Execute(Guid id, UpdateEmployeeRequest request)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        if (employee is null)
            throw new BusinessException("Funcionário não encontrado.");

        employee.Update(request.Name, request.Status);
        await _employeeRepository.UpdateAsync(employee);

        return new EmployeeResponse(employee.Id, employee.Name, employee.Cpf, employee.Status, employee.UpdatedAt);
    }
}