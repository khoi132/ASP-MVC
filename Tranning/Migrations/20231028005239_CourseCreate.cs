using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tranning.Migrations
{
    /// <inheritdoc />
    public partial class CourseCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.CreateTable(
                name: "TrainerTestCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Description = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Icon = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Status = table.Column<int>(type: "Integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerTestCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    TrainerTestCategoriesId = table.Column<int>(type: "int", nullable: true),
                    NameCourse = table.Column<string>(type: "Varchar(50)", nullable: true),
                    Description = table.Column<string>(type: "Varchar(100)", nullable: true),
                    Startdate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    Enddate = table.Column<DateTime>(type: "DateTime", nullable: true),
                    Vote = table.Column<int>(type: "Integer", nullable: false),
                    Avatar = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Status = table.Column<int>(type: "Integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_TrainerTestCategories_TrainerTestCategoriesId",
                        column: x => x.TrainerTestCategoriesId,
                        principalTable: "TrainerTestCategories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TrainerTestCategoriesId",
                table: "Courses",
                column: "TrainerTestCategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "TrainerTestCategories");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "Varchar(100)", nullable: false),
                    Icon = table.Column<string>(type: "Varchar(50)", nullable: false),
                    NameRoles = table.Column<string>(type: "Varchar(50)", nullable: false),
                    Status = table.Column<int>(type: "Integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });
        }
    }
}
