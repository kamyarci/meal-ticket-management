using MealTicketManagement.Application.DTOs.Employee;
using MealTicketManagement.Application.Interfaces;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using EmployeeEntity = MealTicketManagement.Domain.Entities.Employee;

namespace MealTicketManagement.Application.UseCases.Employee;

public class CreateEmployeeUseCase : ICreateEmployeeUseCase
{
    private readonly IEmployeeRepository _employeeRepository;

    public CreateEmployeeUseCase(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<EmployeeResponse> Execute(CreateEmployeeRequest request)
    {
        if (await _employeeRepository.ExistsCpfAsync(request.Cpf))
            throw new BusinessException("CPF já cadastrado.");

        var employee = new EmployeeEntity(request.Name, request.Cpf);
        await _employeeRepository.AddAsync(employee);

        return new EmployeeResponse(employee.Id, employee.Name, employee.Cpf, employee.Status, employee.UpdatedAt);
    }
}