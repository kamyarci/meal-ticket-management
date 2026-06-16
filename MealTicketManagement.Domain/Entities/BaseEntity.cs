using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; private set; }
    public Status Status { get; protected set; }
    public DateTime UpdatedAt { get; private set; }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
        Status = Status.Active;
        UpdatedAt = DateTime.UtcNow;
    }

    protected void SetUpdatedAt() => UpdatedAt = DateTime.UtcNow;
}