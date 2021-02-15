using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class User2_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentedCars_User_UserID",
                table: "RentedCars");

            migrationBuilder.DropForeignKey(
                name: "FK_User_Images_ImageID",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_User_ImageID",
                table: "Users",
                newName: "IX_Users_ImageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RentedCars_Users_UserID",
                table: "RentedCars",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Images_ImageID",
                table: "Users",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentedCars_Users_UserID",
                table: "RentedCars");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Images_ImageID",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameIndex(
                name: "IX_Users_ImageID",
                table: "User",
                newName: "IX_User_ImageID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_RentedCars_User_UserID",
                table: "RentedCars",
                column: "UserID",
                principalTable: "User",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_User_Images_ImageID",
                table: "User",
                column: "ImageID",
                principalTable: "Images",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
