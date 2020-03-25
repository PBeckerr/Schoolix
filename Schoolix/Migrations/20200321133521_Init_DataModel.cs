using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Schoolix.Migrations
{
    public partial class Init_DataModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    Url = table.Column<string>(maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SchoolYears",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Begin = table.Column<DateTime>(type: "date", nullable: false),
                    End = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    CourseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Grade = table.Column<byte>(nullable: false),
                    Label = table.Column<string>(maxLength: 20, nullable: false),
                    SchoolYearId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Classes_SchoolYears_SchoolYearId",
                        column: x => x.SchoolYearId,
                        principalTable: "SchoolYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExerciseFiles",
                columns: table => new
                {
                    ExerciseId = table.Column<Guid>(nullable: false),
                    FileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseFiles", x => new { x.ExerciseId, x.FileId });
                    table.ForeignKey(
                        name: "FK_ExerciseFiles_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseFiles_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Submissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    ExerciseId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submissions_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseClassRelations",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(nullable: false),
                    ClassId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseClassRelations", x => new { x.CourseId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_CourseClassRelations_Classes_ClassId",
                        column: x => x.ClassId,
                        principalTable: "Classes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseClassRelations_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubmissionFiles",
                columns: table => new
                {
                    SubmissionId = table.Column<Guid>(nullable: false),
                    FileId = table.Column<Guid>(nullable: false),
                    DbSubmissionId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubmissionFiles", x => new { x.SubmissionId, x.FileId });
                    table.ForeignKey(
                        name: "FK_SubmissionFiles_Submissions_DbSubmissionId",
                        column: x => x.DbSubmissionId,
                        principalTable: "Submissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SubmissionFiles_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SchoolYearId",
                table: "Classes",
                column: "SchoolYearId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseClassRelations_ClassId",
                table: "CourseClassRelations",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseFiles_FileId",
                table: "ExerciseFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CourseId",
                table: "Exercises",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFiles_DbSubmissionId",
                table: "SubmissionFiles",
                column: "DbSubmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubmissionFiles_FileId",
                table: "SubmissionFiles",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Submissions_ExerciseId",
                table: "Submissions",
                column: "ExerciseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseClassRelations");

            migrationBuilder.DropTable(
                name: "ExerciseFiles");

            migrationBuilder.DropTable(
                name: "SubmissionFiles");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Submissions");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "SchoolYears");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
