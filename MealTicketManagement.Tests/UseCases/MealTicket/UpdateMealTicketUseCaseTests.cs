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

public class UpdateMealTicketUseCaseTests
{
    private readonly Mock<IMealTicketRepository> _mealTicketRepositoryMock;
    private readonly UpdateMealTicketUseCase _useCase;

    public UpdateMealTicketUseCaseTests()
    {
        _mealTicketRepositoryMock = new Mock<IMealTicketRepository>();
        _useCase = new UpdateMealTicketUseCase(_mealTicketRepositoryMock.Object);
    }

    [Fact]
    public async Task Execute_WithNonExistentTicket_ShouldThrowBusinessException()
    {
        _mealTicketRepositoryMock
            .Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync((MealTicketEntity?)null);

        await Assert.ThrowsAsync<BusinessException>(() =>
            _useCase.Execute(Guid.NewGuid(), new UpdateMealTicketRequest(10, Status.Active)));
    }

    [Fact]
    public async Task Execute_WithValidData_ShouldReturnUpdatedMealTicketResponse()
    {
        EmployeeEntity employee = new EmployeeEntity("Ariel", "17552035005");
        MealTicketEntity ticket = new MealTicketEntity(employee, 5);

        _mealTicketRepositoryMock
            .Setup(r => r.GetByIdAsync(ticket.Id))
            .ReturnsAsync(ticket);

        MealTicketResponse result = await _useCase.Execute(ticket.Id, new UpdateMealTicketRequest(10, Status.Active));

        Assert.Equal(10, result.Quantity);
        _mealTicketRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<MealTicketEntity>()), Times.Once);
    }
    
    [Fact]
    public async Task Execute_WithZeroQuantity_ShouldThrowBusinessException()
    {
        EmployeeEntity employee = new EmployeeEntity("Ariel", "17552035005");
        MealTicketEntity ticket = new MealTicketEntity(employee, 5);

        _mealTicketRepositoryMock
            .Setup(r => r.GetByIdAsync(ticket.Id))
            .ReturnsAsync(ticket);

        await Assert.ThrowsAsync<BusinessException>(() =>
            _useCase.Execute(ticket.Id, new UpdateMealTicketRequest(0, Status.Active)));
    }
}