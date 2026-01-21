using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DiaryApp.Migrations
{
    /// <inheritdoc />
    public partial class SeedDiaryEntry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: -3);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: -2);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.InsertData(
                table: "DiaryEntries",
                columns: new[] { "Id", "Content", "Created", "Title" },
                values: new object[,]
                {
                    { 1, "Went to the restaurant to eat with my friends. We ate hamburguers, pizzas, only junk food", new DateTime(2025, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Restaurant with my Friends" },
                    { 2, "Today, I learned many things about the back-end development, mainly how does MVC work", new DateTime(2025, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Learning back-end" },
                    { 3, "I've started practicing my knowledge at coding, designing some projects on the front-end. Then, I'm willing to create the back-end logic, finally putting my knowledge on practice", new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Developing some projects" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DiaryEntries",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "DiaryEntries",
                columns: new[] { "Id", "Content", "Created", "Title" },
                values: new object[,]
                {
                    { -3, "I've started practicing my knowledge at coding, designing some projects on the front-end. Then, I'm willing to create the back-end logic, finally putting my knowledge on practice", new DateTime(2025, 10, 17, 10, 31, 9, 318, DateTimeKind.Local).AddTicks(1551), "Developing some projects" },
                    { -2, "Today, I learned many things about the back-end development, mainly how does MVC work", new DateTime(2025, 10, 17, 10, 31, 9, 318, DateTimeKind.Local).AddTicks(1545), "Learning back-end" },
                    { -1, "Went to the restaurant to eat with my friends. We ate hamburguers, pizzas, only junk food", new DateTime(2025, 10, 17, 10, 31, 9, 314, DateTimeKind.Local).AddTicks(4565), "Restaurant with my Friends" }
                });
        }
    }
}
