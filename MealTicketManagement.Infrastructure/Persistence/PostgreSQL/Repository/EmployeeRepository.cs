using MealTicketManagement.Domain.Entities;
using MealTicketManagement.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Context;

namespace MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Repository;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _dbContext;

    public EmployeeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Employee employee)
    {
        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Employee employee)
    {
        _dbContext.Employees.Update(employee);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        Employee? employee = await _dbContext.Employees.FindAsync(id);
        return employee;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        List<Employee> employees = await _dbContext.Employees.ToListAsync();
        return employees;
    }

    public async Task<bool> ExistsCpfAsync(string cpf)
    {
        return await _dbContext.Employees.AnyAsync(e => e.Cpf == cpf);
    }
}