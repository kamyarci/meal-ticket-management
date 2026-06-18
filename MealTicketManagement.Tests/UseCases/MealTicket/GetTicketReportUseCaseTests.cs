using MealTicketManagement.Application.DTOs.Report;
using Moq;
using MealTicketManagement.Application.UseCases.MealTicket;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using EmployeeEntity = MealTicketManagement.Domain.Entities.Employee;
using MealTicketEntity = MealTicketManagement.Domain.Entities.MealTicket;

namespace MealTicketManagement.Tests.UseCases.MealTicket;

public class GetTicketReportUseCaseTests
{
    private readonly Mock<IMealTicketRepository> _repositoryMock;
    private readonly GetTicketReportUseCase _useCase;

    public GetTicketReportUseCaseTests()
    {
        _repositoryMock = new Mock<IMealTicketRepository>();
        _useCase = new GetTicketReportUseCase(_repositoryMock.Object);
    }

    [Fact]
    public async Task Execute_WithStartDateGreaterThanEndDate_ShouldThrowBusinessException()
    {
        DateTime startDate = new DateTime(2026, 6, 19);
        DateTime endDate = new DateTime(2026, 6, 18);

        await Assert.ThrowsAsync<BusinessException>(() => _useCase.Execute(startDate, endDate));
    }

    [Fact]
    public async Task Execute_WithValidPeriod_ShouldReturnGroupedReport()
    {
        EmployeeEntity employee1 = new EmployeeEntity("Ariel", "17552035005");
        EmployeeEntity employee2 = new EmployeeEntity("Kamyla", "09620423070");

        List<MealTicketEntity> tickets = new List<MealTicketEntity>
        {
            new(employee1, 10),
            new(employee1, 5),
            new(employee2, 8)
        };

        DateTime startDate = new DateTime(2026, 6, 18);
        DateTime endDate = new DateTime(2026, 6, 19);

        _repositoryMock.Setup(r => r.GetByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(tickets);

        TicketReportResponse response = await _useCase.Execute(startDate, endDate);

        Assert.Equal(23, response.TotalTickets);
        Assert.Equal(2, response.TotalEmployees);
        Assert.Equal(15, response.Employees.First(e => e.Name == "Ariel").TotalTickets);
        Assert.Equal(8, response.Employees.First(e => e.Name == "Kamyla").TotalTickets);
    }

    [Fact]
    public async Task Execute_WithNoTicketsInPeriod_ShouldReturnEmptyReport()
    {
        _repositoryMock.Setup(r => r.GetByPeriodAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
            .ReturnsAsync(new List<MealTicketEntity>());

        DateTime startDate = new DateTime(2026, 6, 18);
        DateTime endDate = new DateTime(2026, 6, 19);

        TicketReportResponse response = await _useCase.Execute(startDate, endDate);

        Assert.Equal(0, response.TotalTickets);
        Assert.Equal(0, response.TotalEmployees);
        Assert.Empty(response.Employees);
    }
}