using MealTicketManagement.Domain.Entities;

namespace MealTicketManagement.Domain.Interfaces;

public interface IMealTicketRepository : IRepository<MealTicket, Guid>
{
    Task<IEnumerable<MealTicket>> GetByPeriodAsync(DateTime start, DateTime end);
}