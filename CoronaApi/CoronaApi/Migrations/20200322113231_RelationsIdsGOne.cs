using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoronaApi.Migrations
{
    public partial class RelationsIdsGOne : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseStudentRelations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CourseClassRelations");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClassStudentRelations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CourseStudentRelations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CourseClassRelations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ClassStudentRelations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
