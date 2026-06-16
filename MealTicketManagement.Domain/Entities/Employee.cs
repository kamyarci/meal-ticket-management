using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Domain.Entities;

public class Employee
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;
    public Status Status { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public Employee()
    {
    }

    public Employee(string name, string cpf)
    {
        Id = Guid.NewGuid();
        Name = name;
        Cpf = cpf;
        Status = Status.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Update(string name, Status status)
    {
        Name = name;
        Status = status;
        UpdatedAt = DateTime.UtcNow;
    }
}