using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChordDatabase.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedCustomEnumsToModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quality",
                table: "Chord",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Inversion",
                table: "Chord",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inversion",
                table: "Chord");

            migrationBuilder.AlterColumn<string>(
                name: "Quality",
                table: "Chord",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
