using MealTicketManagement.Domain.Enums;
using MealTicketManagement.Domain.Guards;

namespace MealTicketManagement.Domain.Entities;

public class MealTicket : BaseEntity
{
    public Employee Employee { get; private set; } = null!;
    public int Quantity { get; private set; }
    public DateTime DeliveredAt { get; private set; }

    public MealTicket()
    {
    }

    public MealTicket(Employee employee, int quantity)
    {
        Guard.IsNotNull(employee, "Funcionário é obrigatório.");
        Employee = employee;
        Quantity = quantity;
        DeliveredAt = DateTime.UtcNow;
    }

    public void Update(int quantity, Status status)
    {
        Guard.IsGreaterThan(quantity, 0, "A quantidade deve ser maior que zero.");
        Quantity = quantity;
        Status = status;
        SetUpdatedAt();
    }
}