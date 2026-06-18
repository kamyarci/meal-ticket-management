using MealTicketManagement.Application.DTOs.Employee.Request;
using MealTicketManagement.Application.DTOs.Employee.Response;
using MealTicketManagement.Application.UseCases.Employee;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using EmployeeEntity = MealTicketManagement.Domain.Entities.Employee;
using Moq;

namespace MealTicketManagement.Tests.UseCases.Employee;

public class CreateEmployeeUseCaseTests
{
    private readonly Mock<IEmployeeRepository> _repositoryMock;
    private readonly CreateEmployeeUseCase _useCase;

    public CreateEmployeeUseCaseTests()
    {
        _repositoryMock = new Mock<IEmployeeRepository>();
        _useCase = new CreateEmployeeUseCase(_repositoryMock.Object);
    }

    [Fact]
    public async Task Execute_WithDuplicateCpf_ShouldThrowBusinessException()
    {
        _repositoryMock
            .Setup(r => r.ExistsCpfAsync("17552035005"))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<BusinessException>(() =>
            _useCase.Execute(new CreateEmployeeRequest("Ariel", "17552035005")));
    }

    [Fact]
    public async Task Execute_WithValidData_ShouldReturnEmployeeResponse()
    {
        _repositoryMock
            .Setup(r => r.ExistsCpfAsync("17552035005"))
            .ReturnsAsync(false);

        EmployeeResponse result = await _useCase.Execute(new CreateEmployeeRequest("Ariel", "17552035005"));

        Assert.Equal("Ariel", result.Name);
        Assert.Equal("17552035005", result.Cpf);
        _repositoryMock.Verify(r => r.AddAsync(It.IsAny<EmployeeEntity>()), Times.Once);
    }
}