using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class addedAvailableForrent_cars_colomn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AvailableForRent",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableForRent",
                table: "Cars");
        }
    }
}
