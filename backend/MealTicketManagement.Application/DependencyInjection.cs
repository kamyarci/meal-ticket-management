using MealTicketManagement.Application.Interfaces.Employee;
using MealTicketManagement.Application.Interfaces.MealTicket;
using MealTicketManagement.Application.UseCases.Employee;
using MealTicketManagement.Application.UseCases.MealTicket;
using Microsoft.Extensions.DependencyInjection;

namespace MealTicketManagement.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICreateEmployeeUseCase, CreateEmployeeUseCase>();
        services.AddScoped<IUpdateEmployeeUseCase, UpdateEmployeeUseCase>();
        services.AddScoped<IGetEmployeeByIdUseCase, GetEmployeeByIdUseCase>();
        services.AddScoped<IGetAllEmployeesUseCase, GetAllEmployeesUseCase>();

        services.AddScoped<ICreateMealTicketUseCase, CreateMealTicketUseCase>();
        services.AddScoped<IUpdateMealTicketUseCase, UpdateMealTicketUseCase>();
        services.AddScoped<IGetMealTicketByIdUseCase, GetMealTicketByIdUseCase>();
        services.AddScoped<IGetTicketReportUseCase, GetTicketReportUseCase>();

        return services;
    }
}