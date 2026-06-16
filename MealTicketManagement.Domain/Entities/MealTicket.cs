using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Domain.Entities;

public class MealTicket
{
    public Guid Id { get; private set; }
    public Employee Employee { get; private set; } = null!;
    public int Quantity { get; private set; }
    public Status Status { get; private set; }
    public DateTime DeliveredAt { get; private set; }

    public MealTicket()
    {
    }

    public MealTicket(Employee employee, int quantity)
    {
        Id = Guid.NewGuid();
        Employee = employee;
        Quantity = quantity;
        Status = Status.Active;
        DeliveredAt = DateTime.UtcNow;
    }

    public void Update(int quantity, Status status)
    {
        Quantity = quantity;
        Status = status;
    }
}