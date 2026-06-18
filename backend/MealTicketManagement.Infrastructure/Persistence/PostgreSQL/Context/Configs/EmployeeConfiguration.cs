using MealTicketManagement.Domain.Entities;
using MealTicketManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealTicketManagement.Infrastructure.Persistence.PostgreSQL.Context.Configs;

public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.ToTable("employees");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedOnAdd();
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(p => p.Cpf)
            .IsRequired()
            .HasMaxLength(11);
        builder.HasIndex(p => p.Cpf).IsUnique();
        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion(v => v.ToString()[0].ToString(), v => (Status)v[0])
            .HasMaxLength(1);

        builder.Property(p => p.UpdatedAt)
            .IsRequired();
    }
}