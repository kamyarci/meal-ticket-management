using MealTicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Context;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<MealTicket> MealTickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}