using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tranning.Migrations
{
    /// <inheritdoc />
    public partial class ChangDataTypeColumnCategoriesCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "TrainerTestCategories",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeletedAt",
                table: "TrainerTestCategories",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "TrainerTestCategories",
                type: "datetime",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UpdatedAt",
                table: "TrainerTestCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            

            migrationBuilder.AlterColumn<string>(
                name: "DeletedAt",
                table: "TrainerTestCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedAt",
                table: "TrainerTestCategories",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime");
        }
    }
}
