using MealTicketManagement.Domain.Entities;

namespace MealTicketManagement.Domain.Interfaces;

public interface IEmployeeRepository : IRepository<Employee, Guid>
{
    Task<bool> ExistsCpfAsync(string cpf);
}