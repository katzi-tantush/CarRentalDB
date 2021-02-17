using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class simpleCrudUpdate_car_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Branches_BranchID",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarCategories_CarCategoryID",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Images_ImageID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_BranchID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarCategoryID",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_ImageID",
                table: "Cars");

            migrationBuilder.AlterColumn<int>(
                name: "ImageID",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CarCategoryID",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ImageID",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CarCategoryID",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BranchID",
                table: "Cars",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_BranchID",
                table: "Cars",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarCategoryID",
                table: "Cars",
                column: "CarCategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Cars_ImageID",
                table: "Cars",
                column: "ImageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Branches_BranchID",
                table: "Cars",
                column: "BranchID",
                principalTable: "Branches",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarCategories_CarCategoryID",
                table: "Cars",
                column: "CarCategoryID",
                principalTable: "CarCategories",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Images_ImageID",
                table: "Cars",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
