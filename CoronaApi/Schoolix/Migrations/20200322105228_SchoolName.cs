using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolix.Migrations
{
    public partial class SchoolName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Schools",
                nullable: true);

            migrationBuilder.Sql($"update Schools set Name = 'ChangeMe'");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CourseStudentRelations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "CourseClassRelations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "ClassStudentRelations",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Schools");

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
    }
}
