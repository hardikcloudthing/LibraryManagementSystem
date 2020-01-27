using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryDataContext.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckInOutHistories_Books_BookId",
                table: "CheckInOutHistories");

            migrationBuilder.DropIndex(
                name: "IX_CheckInOutHistories_BookId",
                table: "CheckInOutHistories");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "CheckInOutHistories");

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

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "CheckInOutHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CheckInOutHistories_BookId",
                table: "CheckInOutHistories",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckInOutHistories_Books_BookId",
                table: "CheckInOutHistories",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
