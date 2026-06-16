using MealTicketManagement.Domain.Enums;
using MealTicketManagement.Domain.Guards;

namespace MealTicketManagement.Domain.Entities;

public class Employee : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Cpf { get; private set; } = string.Empty;

    public Employee()
    {
    }

    public Employee(string name, string cpf)
    {
        Guard.IsNotNullOrEmpty(name, "O nome é obrigatório.");
        Guard.IsCpfValid(cpf);
        Name = name;
        Cpf = cpf;
    }

    public void Update(string name, Status status)
    {
        Guard.IsNotNullOrEmpty(name, "O nome é obrigatório.");
        Name = name;
        Status = status;
        SetUpdatedAt();
    }
}