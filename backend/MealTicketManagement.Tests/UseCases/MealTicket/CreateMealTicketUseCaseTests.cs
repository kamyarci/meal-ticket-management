using MealTicketManagement.Application.DTOs.MealTicket.Request;
using MealTicketManagement.Application.DTOs.MealTicket.Response;
using MealTicketManagement.Application.UseCases.MealTicket;
using MealTicketManagement.Domain.Enums;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using Moq;
using EmployeeEntity = MealTicketManagement.Domain.Entities.Employee;
using MealTicketEntity = MealTicketManagement.Domain.Entities.MealTicket;

namespace MealTicketManagement.Tests.UseCases.MealTicket;

public class CreateMealTicketUseCaseTests
{
    private readonly Mock<IMealTicketRepository> _mealTicketRepositoryMock;
    private readonly Mock<IEmployeeRepository> _employeeRepositoryMock;
    private readonly CreateMealTicketUseCase _useCase;

    public CreateMealTicketUseCaseTests()
    {
        _mealTicketRepositoryMock = new Mock<IMealTicketRepository>();
        _employeeRepositoryMock = new Mock<IEmployeeRepository>();
        _useCase = new CreateMealTicketUseCase(_mealTicketRepositoryMock.Object, _employeeRepositoryMock.Object);
    }

    [Fact]
    public async Task Execute_WithNonExistentEmployee_ShouldThrowBusinessException()
    {
        _employeeRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((EmployeeEntity?)null);

        await Assert.ThrowsAsync<BusinessException>(() =>
            _useCase.Execute(new CreateMealTicketRequest(Guid.NewGuid(), 5)));
    }

    [Fact]
    public async Task Execute_WithInactiveEmployee_ShouldThrowBusinessException()
    {
        EmployeeEntity employee = new EmployeeEntity("Ariel", "17552035005");
        employee.Update("Ariel", Status.Inactive);

        _employeeRepositoryMock
            .Setup(r => r.GetByIdAsync(employee.Id))
            .ReturnsAsync(employee);

        await Assert.ThrowsAsync<BusinessException>(() =>
            _useCase.Execute(new CreateMealTicketRequest(employee.Id, 5)));
    }

    [Fact]
    public async Task Execute_WithValidData_ShouldReturnMealTicketResponse()
    {
        EmployeeEntity employee = new EmployeeEntity("Ariel", "17552035005");

        _employeeRepositoryMock
            .Setup(r => r.GetByIdAsync(employee.Id))
            .ReturnsAsync(employee);

        MealTicketResponse result = await _useCase.Execute(new CreateMealTicketRequest(employee.Id, 5));

        Assert.Equal(5, result.Quantity);
        _mealTicketRepositoryMock.Verify(r => r.AddAsync(It.IsAny<MealTicketEntity>()), Times.Once);
    }
}