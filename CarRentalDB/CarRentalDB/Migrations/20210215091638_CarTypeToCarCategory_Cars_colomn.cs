using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class CarTypeToCarCategory_Cars_colomn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarCategories_CarTypeID",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "CarTypeID",
                table: "Cars",
                newName: "CarCategoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CarTypeID",
                table: "Cars",
                newName: "IX_Cars_CarCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarCategories_CarCategoryID",
                table: "Cars",
                column: "CarCategoryID",
                principalTable: "CarCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarCategories_CarCategoryID",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "CarCategoryID",
                table: "Cars",
                newName: "CarTypeID");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_CarCategoryID",
                table: "Cars",
                newName: "IX_Cars_CarTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarCategories_CarTypeID",
                table: "Cars",
                column: "CarTypeID",
                principalTable: "CarCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
