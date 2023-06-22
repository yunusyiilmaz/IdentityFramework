using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ProjectExamDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("13f3f0bc-08f6-4fe3-81a8-c5e631c923e4"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("42ab4cc9-dae4-42d2-aece-f08543a1bba3"), 0, "6083b536-d407-4e6a-9f8e-cd6c25ad3bcb", "admin@gmail.com", false, false, new DateTimeOffset(new DateTime(2023, 3, 29, 12, 32, 43, 54, DateTimeKind.Unspecified).AddTicks(6885), new TimeSpan(0, 0, 0, 0, 0)), "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEIDbmMYaKotiO6zckoJSq8osd7FaovV4Mk09M0NzyzjoA1BbusKsvqeNdIhkb0SWHA==", "1111111111", false, "7c0f5aff-ffea-42ee-96b3-d8571eb51713", false, "admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("42ab4cc9-dae4-42d2-aece-f08543a1bba3"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("13f3f0bc-08f6-4fe3-81a8-c5e631c923e4"), 0, "293ba05f-9caf-4031-b7b2-c77c354cf18f", "admin@gmail.com", false, false, new DateTimeOffset(new DateTime(2022, 12, 21, 5, 50, 33, 90, DateTimeKind.Unspecified).AddTicks(5699), new TimeSpan(0, 0, 0, 0, 0)), "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEBar0zvHeT37++i607uBEVaeWWHTsoI5DlWUY1dR3z7SV/9T+h9CmApqERJvN3tDeg==", "1111111111", false, "3b74d709-829b-4207-88d8-be7582179d44", false, "admin" });
        }
    }
}
