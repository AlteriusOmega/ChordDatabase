using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChordDatabase.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Alterations = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scale = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Functions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Feeling = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chord", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chord");
        }
    }
}
