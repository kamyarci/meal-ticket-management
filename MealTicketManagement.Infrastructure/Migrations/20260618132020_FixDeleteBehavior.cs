using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MealTicketManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixDeleteBehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meal_tickets_employees_EmployeeId",
                table: "meal_tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_meal_tickets_employees_EmployeeId",
                table: "meal_tickets",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_meal_tickets_employees_EmployeeId",
                table: "meal_tickets");

            migrationBuilder.AddForeignKey(
                name: "FK_meal_tickets_employees_EmployeeId",
                table: "meal_tickets",
                column: "EmployeeId",
                principalTable: "employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
