using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryDataContext.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CId",
                table: "Borrowers");

            migrationBuilder.AddColumn<bool>(
                name: "BookIsRemoved",
                table: "Books",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookIsRemoved",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "CId",
                table: "Borrowers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
