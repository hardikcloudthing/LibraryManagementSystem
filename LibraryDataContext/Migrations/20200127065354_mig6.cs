using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryDataContext.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckInOutHistoryId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_CheckInOutHistoryId",
                table: "Books",
                column: "CheckInOutHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_CheckInOutHistories_CheckInOutHistoryId",
                table: "Books",
                column: "CheckInOutHistoryId",
                principalTable: "CheckInOutHistories",
                principalColumn: "CheckInOutHistoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_CheckInOutHistories_CheckInOutHistoryId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_CheckInOutHistoryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CheckInOutHistoryId",
                table: "Books");
        }
    }
}
