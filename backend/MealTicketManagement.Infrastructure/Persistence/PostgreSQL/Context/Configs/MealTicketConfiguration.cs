using MealTicketManagement.Domain.Entities;
using MealTicketManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Context.Configs;

public class MealTicketConfiguration : IEntityTypeConfiguration<MealTicket>
{
    public void Configure(EntityTypeBuilder<MealTicket> builder)
    {
        builder.ToTable("meal_tickets");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Quantity)
            .IsRequired();
        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion(v => v.ToString()[0].ToString(), v => (Status)v[0])
            .HasMaxLength(1);
        builder.Property(p => p.DeliveredAt)
            .IsRequired();
        builder.HasOne(p => p.Employee)
            .WithMany()
            .HasForeignKey("EmployeeId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}