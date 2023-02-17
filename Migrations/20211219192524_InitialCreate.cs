using Microsoft.EntityFrameworkCore.Migrations;

namespace ForzaColourSearch.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicle",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Make = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColourName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COLOURX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaturationX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrightnessX = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    COLOURY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SaturationY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrightnessY = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicle", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicle");
        }
    }
}
