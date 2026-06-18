using System.Text.Json.Serialization;
using MealTicketManagement.Api.Extensions;
using MealTicketManagement.Api.Middlewares;
using MealTicketManagement.Application;
using MealTicketManagement.Infrastructure;
using MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using Serilog;

namespace MealTicketManagement.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(allowIntegerValues: false)))
            .ConfigureApiBehaviorOptions(options =>
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Key != "request")
                        .SelectMany(e => e.Value!.Errors.Select(x => ModelStateErrorMapper.Map(e.Key, x)))
                        .Distinct()
                        .ToList();

                    return new BadRequestObjectResult(new { message = string.Join("; ", errors) });
                });

        builder.Services.AddCors(options =>
            options.AddDefaultPolicy(policy =>
                policy.WithOrigins(builder.Configuration["Cors:AllowedOrigin"]!)
                      .AllowAnyHeader()
                      .AllowAnyMethod()));

        builder.Services.AddAuthorization();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseSerilogRequestLogging();
        app.UseCors();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        using (IServiceScope scope = app.Services.CreateScope())
        {
            AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            ResiliencePipeline pipeline = new ResiliencePipelineBuilder()
                .AddRetry(new RetryStrategyOptions
                {
                    MaxRetryAttempts = 5,
                    Delay = TimeSpan.FromSeconds(5)
                })
                .Build();

            await pipeline.ExecuteAsync(async _ =>
            {
                Log.Information("Tentando aplicar migrations...");
                await db.Database.MigrateAsync();
                Log.Information("Migrations aplicadas com sucesso.");
            });
        }

        app.Run();
    }
}