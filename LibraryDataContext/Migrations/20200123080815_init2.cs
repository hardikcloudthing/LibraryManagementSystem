using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryDataContext.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authors_Books_BookId",
                table: "Authors");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthoId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Authors_BookId",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Authors");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthoId",
                table: "Books",
                column: "AuthoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_AuthoId",
                table: "Books");

            migrationBuilder.AddColumn<Guid>(
                name: "BookId",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthoId",
                table: "Books",
                column: "AuthoId",
                unique: true,
                filter: "[AuthoId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Authors_BookId",
                table: "Authors",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authors_Books_BookId",
                table: "Authors",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
