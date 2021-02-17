using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class ProductionDateDailyCostAndOverdueDailyCost_CarCategories_colomns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DailyCost",
                table: "CarCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverdueDailyCost",
                table: "CarCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionDate",
                table: "CarCategories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DailyCost",
                table: "CarCategories");

            migrationBuilder.DropColumn(
                name: "OverdueDailyCost",
                table: "CarCategories");

            migrationBuilder.DropColumn(
                name: "ProductionDate",
                table: "CarCategories");
        }
    }
}
