using MealTicketManagement.Application.DTOs.Report;

namespace MealTicketManagement.Application.Interfaces.MealTicket;

public interface IGetTicketReportUseCase
{
    Task<TicketReportResponse> Execute(DateTime startDate, DateTime endDate);
}