using MealTicketManagement.Domain.Interfaces;
using MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Context;
using MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MealTicketManagement.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IMealTicketRepository, MealTicketRepository>();

        return services;
    }
}