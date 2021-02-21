using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class dropRentReady_cars_colomn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentReady",
                table: "Cars");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RentReady",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
