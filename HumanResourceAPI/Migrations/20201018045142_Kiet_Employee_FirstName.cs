using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanResourceAPI.Migrations
{
    public partial class Kiet_Employee_FirstName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("af7f7186-de0b-4894-9b39-9e4164718f70"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("e8ccde50-0ab6-40f8-af56-f3d87415efa6"));

            migrationBuilder.DropColumn(
                name: "FistName",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Employees",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[] { new Guid("e17d4462-52e8-41f7-b000-80709c445fee"), "Duy Tan Street, Dich Vong Hau Ward, Cau Giay District, Hanoi City, Vietnam", "Vietnam", "FPT Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[] { new Guid("d04ebedc-f38d-4141-b62b-aa9c8554ecbe"), "312 Forest Avenue, California", "USA", "Apple Ltd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("d04ebedc-f38d-4141-b62b-aa9c8554ecbe"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("e17d4462-52e8-41f7-b000-80709c445fee"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "FistName",
                table: "Employees",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[] { new Guid("af7f7186-de0b-4894-9b39-9e4164718f70"), "Duy Tan Street, Dich Vong Hau Ward, Cau Giay District, Hanoi City, Vietnam", "Vietnam", "FPT Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "Country", "Name" },
                values: new object[] { new Guid("e8ccde50-0ab6-40f8-af56-f3d87415efa6"), "312 Forest Avenue, California", "USA", "Apple Ltd" });
        }
    }
}
