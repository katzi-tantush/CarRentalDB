using Microsoft.EntityFrameworkCore.Migrations;

namespace CarRentalDB.Migrations
{
    public partial class Car_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarTypeID = table.Column<int>(type: "int", nullable: true),
                    KillometerCount = table.Column<int>(type: "int", nullable: false),
                    RentReady = table.Column<bool>(type: "bit", nullable: false),
                    AvailableForRent = table.Column<bool>(type: "bit", nullable: false),
                    LicensePlateNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cars_CarCategories_CarTypeID",
                        column: x => x.CarTypeID,
                        principalTable: "CarCategories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarTypeID",
                table: "Cars",
                column: "CarTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}
