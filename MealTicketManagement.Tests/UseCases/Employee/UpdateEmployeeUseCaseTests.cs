using Moq;
using MealTicketManagement.Application.DTOs.Employee.Request;
using MealTicketManagement.Application.DTOs.Employee.Response;
using MealTicketManagement.Application.UseCases.Employee;
using MealTicketManagement.Domain.Enums;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using EmployeeEntity = MealTicketManagement.Domain.Entities.Employee;

namespace MealTicketManagement.Tests.UseCases.Employee;

public class UpdateEmployeeUseCaseTests
{
    private readonly Mock<IEmployeeRepository> _repositoryMock;
    private readonly UpdateEmployeeUseCase _useCase;

    public UpdateEmployeeUseCaseTests()
    {
        _repositoryMock = new Mock<IEmployeeRepository>();
        _useCase = new UpdateEmployeeUseCase(_repositoryMock.Object);
    }

    [Fact]
    public async Task Execute_WithNonExistentEmployee_ShouldThrowBusinessException()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((EmployeeEntity?)null);

        UpdateEmployeeRequest request = new UpdateEmployeeRequest("Ariel", Status.Active);

        await Assert.ThrowsAsync<BusinessException>(() => _useCase.Execute(Guid.NewGuid(), request));
    }

    [Fact]
    public async Task Execute_WithValidData_ShouldReturnUpdatedEmployeeResponse()
    {
        EmployeeEntity employee = new EmployeeEntity("Ariel", "17552035005");
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(employee);

        UpdateEmployeeRequest request = new UpdateEmployeeRequest("Ariel Atualizado", Status.Inactive);

        EmployeeResponse response = await _useCase.Execute(employee.Id, request);

        Assert.Equal("Ariel Atualizado", response.Name);
        Assert.Equal(Status.Inactive, response.Status);
        _repositoryMock.Verify(r => r.UpdateAsync(employee), Times.Once);
    }

    [Fact]
    public async Task Execute_WithEmptyName_ShouldThrowBusinessException()
    {
        EmployeeEntity employee = new EmployeeEntity("Ariel", "17552035005");
        _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(employee);

        UpdateEmployeeRequest request = new UpdateEmployeeRequest("", Status.Active);

        await Assert.ThrowsAsync<BusinessException>(() => _useCase.Execute(employee.Id, request));
    }
}