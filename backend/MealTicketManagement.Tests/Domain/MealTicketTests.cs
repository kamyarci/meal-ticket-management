using MealTicketManagement.Domain.Entities;
using MealTicketManagement.Domain.Enums;
using MealTicketManagement.Domain.Exceptions;

namespace MealTicketManagement.Tests.Domain;

public class MealTicketTests
{
    private Employee CreateValidEmployee() => new("Ariel", "17552035005");

    [Fact]
    public void Create_WithValidData_ShouldSetStatusActive()
    {
        MealTicket ticket = new MealTicket(CreateValidEmployee(), 5);

        Assert.Equal(Status.Active, ticket.Status);
    }

    [Fact]
    public void Create_WithValidData_ShouldSetDeliveredAt()
    {
        DateTime before = DateTime.UtcNow;
        MealTicket ticket = new MealTicket(CreateValidEmployee(), 5);

        Assert.True(ticket.DeliveredAt >= before);
    }

    [Fact]
    public void Create_WithZeroQuantity_ShouldThrowBusinessException()
    {
        Assert.Throws<BusinessException>(() => new MealTicket(CreateValidEmployee(), 0));
    }

    [Fact]
    public void Create_WithNegativeQuantity_ShouldThrowBusinessException()
    {
        Assert.Throws<BusinessException>(() => new MealTicket(CreateValidEmployee(), -1));
    }

    [Fact]
    public void Create_WithNullEmployee_ShouldThrowBusinessException()
    {
        Assert.Throws<BusinessException>(() => new MealTicket(null!, 5));
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateQuantity()
    {
        MealTicket ticket = new MealTicket(CreateValidEmployee(), 5);

        ticket.Update(10, Status.Active);

        Assert.Equal(10, ticket.Quantity);
    }
}