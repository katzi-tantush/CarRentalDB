using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class simpleCrudWithForeignKey_BranchLocationID_colomn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Locations_LocationID",
                table: "Branches");

            migrationBuilder.DropIndex(
                name: "IX_Branches_LocationID",
                table: "Branches");

            migrationBuilder.AlterColumn<int>(
                name: "LocationID",
                table: "Branches",
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
                name: "LocationID",
                table: "Branches",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_LocationID",
                table: "Branches",
                column: "LocationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Branches_Locations_LocationID",
                table: "Branches",
                column: "LocationID",
                principalTable: "Locations",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
