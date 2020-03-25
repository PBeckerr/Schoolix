using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolix.Migrations
{
    public partial class SchoolOwner1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_AspNetUsers_OwnerId1",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_OwnerId1",
                table: "Schools");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Schools");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Schools",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Schools_OwnerId",
                table: "Schools",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_AspNetUsers_OwnerId",
                table: "Schools",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schools_AspNetUsers_OwnerId",
                table: "Schools");

            migrationBuilder.DropIndex(
                name: "IX_Schools_OwnerId",
                table: "Schools");

            migrationBuilder.AlterColumn<Guid>(
                name: "OwnerId",
                table: "Schools",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId1",
                table: "Schools",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schools_OwnerId1",
                table: "Schools",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Schools_AspNetUsers_OwnerId1",
                table: "Schools",
                column: "OwnerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
