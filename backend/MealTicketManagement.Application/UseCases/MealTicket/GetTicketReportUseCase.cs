using MealTicketManagement.Application.DTOs.Report;
using MealTicketManagement.Application.Interfaces.MealTicket;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Interfaces;
using MealTicketEntity = MealTicketManagement.Domain.Entities.MealTicket;

namespace MealTicketManagement.Application.UseCases.MealTicket;

public class GetTicketReportUseCase : IGetTicketReportUseCase
{
    private readonly IMealTicketRepository _mealTicketRepository;

    public GetTicketReportUseCase(IMealTicketRepository mealTicketRepository)
    {
        _mealTicketRepository = mealTicketRepository;
    }

    public async Task<TicketReportResponse> Execute(DateTime startDate, DateTime endDate)
    {
        startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

        if (startDate > endDate)
            throw new BusinessException("A data inicial não pode ser maior que a data final.");

        IEnumerable<MealTicketEntity> tickets = await _mealTicketRepository.GetByPeriodAsync(startDate, endDate);

        List<EmployeeTicketSummary> employeeSummaries = tickets
            .GroupBy(t => t.Employee)
            .Select(g => new EmployeeTicketSummary(g.Key.Id, g.Key.Name, g.Sum(t => t.Quantity)))
            .ToList();

        return new TicketReportResponse(
            startDate,
            endDate,
            employeeSummaries.Sum(e => e.TotalTickets),
            employeeSummaries.Count,
            employeeSummaries);
    }
}