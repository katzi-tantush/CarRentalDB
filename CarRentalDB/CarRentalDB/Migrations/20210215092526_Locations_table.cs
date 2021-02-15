using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class Locations_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocationID",
                table: "Branches",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Longitude = table.Column<int>(type: "int", nullable: false),
                    Latitude = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.ID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branches_Locations_LocationID",
                table: "Branches");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Branches_LocationID",
                table: "Branches");

            migrationBuilder.DropColumn(
                name: "LocationID",
                table: "Branches");
        }
    }
}
