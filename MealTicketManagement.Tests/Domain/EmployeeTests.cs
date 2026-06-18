using MealTicketManagement.Domain.Entities;
using MealTicketManagement.Domain.Exceptions;
using MealTicketManagement.Domain.Enums;

namespace MealTicketManagement.Tests.Domain;

public class EmployeeTests
{
    [Fact]
    public void Create_WithValidData_ShouldSetStatusActive()
    {
        Employee employee = new Employee("Ariel", "17552035005");

        Assert.Equal(Status.Active, employee.Status);
    }

    [Fact]
    public void Create_WithEmptyName_ShouldThrowBusinessException()
    {
        Assert.Throws<BusinessException>(() => new Employee("", "17552035005"));
    }

    [Fact]
    public void Create_WithInvalidCpf_ShouldThrowBusinessException()
    {
        Assert.Throws<BusinessException>(() => new Employee("Ariel", "11111111111"));
    }

    [Fact]
    public void Create_WithInvalidCpfDigits_ShouldThrowBusinessException()
    {
        Assert.Throws<BusinessException>(() => new Employee("Ariel", "12345678901"));
    }

    [Fact]
    public void Create_WithValidData_ShouldSetUpdatedAt()
    {
        DateTime before = DateTime.UtcNow;
        Employee employee = new Employee("Ariel", "17552035005");

        Assert.True(employee.UpdatedAt >= before);
    }

    [Fact]
    public void Update_WithEmptyName_ShouldThrowBusinessException()
    {
        Employee employee = new Employee("Ariel", "17552035005");

        Assert.Throws<BusinessException>(() => employee.Update("", Status.Active));
    }

    [Fact]
    public void Update_WithValidData_ShouldUpdateUpdatedAt()
    {
        Employee employee = new Employee("Ariel", "17552035005");
        DateTime before = DateTime.UtcNow;

        employee.Update("João Santos", Status.Active);

        Assert.True(employee.UpdatedAt >= before);
        Assert.Equal("João Santos", employee.Name);
    }
}