using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryDataContext.Migrations
{
    public partial class mig5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "BookId1",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookId1",
                table: "Books",
                column: "BookId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_CheckInOutHistories_BookId1",
                table: "Books",
                column: "BookId1",
                principalTable: "CheckInOutHistories",
                principalColumn: "CheckInOutHistoryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_CheckInOutHistories_BookId1",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookId1",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookId1",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "CheckInOutHistoryId",
                table: "Books",
                type: "int",
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
    }
}
