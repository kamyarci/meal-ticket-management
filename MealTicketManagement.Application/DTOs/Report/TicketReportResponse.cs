namespace MealTicketManagement.Application.DTOs.Report;

public record EmployeeTicketSummary(Guid EmployeeId, string Name, int TotalTickets);

public record TicketReportResponse(
    DateTime StartDate,
    DateTime EndDate,
    int TotalTickets,
    int TotalEmployees,
    IEnumerable<EmployeeTicketSummary> Employees);