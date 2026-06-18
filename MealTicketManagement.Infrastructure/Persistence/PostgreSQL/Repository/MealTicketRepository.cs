using MealTicketManagement.Domain.Entities;
using MealTicketManagement.Domain.Interfaces;
using MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Context;
using Microsoft.EntityFrameworkCore;

namespace MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Repository;

public class MealTicketRepository : IMealTicketRepository
{
    private readonly AppDbContext _dbContext;

    public MealTicketRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(MealTicket mealTicket)
    {
        await _dbContext.MealTickets.AddAsync(mealTicket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(MealTicket mealTicket)
    {
        _dbContext.MealTickets.Update(mealTicket);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<MealTicket?> GetByIdAsync(Guid id)
    {
        MealTicket? mealTicket = await _dbContext.MealTickets
            .Include(mt => mt.Employee)
            .FirstOrDefaultAsync(mt => mt.Id == id);
        return mealTicket;
    }

    public async Task<IEnumerable<MealTicket>> GetAllAsync()
    {
        List<MealTicket> mealTickets = await _dbContext.MealTickets
            .Include(mt => mt.Employee)
            .ToListAsync();
        return mealTickets;
    }

    public async Task<IEnumerable<MealTicket>> GetByPeriodAsync(DateTime start, DateTime end)
    {
        List<MealTicket> mealTickets = await _dbContext.MealTickets
            .Include(mt => mt.Employee)
            .Where(mt => mt.DeliveredAt >= start && mt.DeliveredAt <= end).ToListAsync();
        return mealTickets;
    }
}