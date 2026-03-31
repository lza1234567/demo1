using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace demo1.Migrations
{
    /// <inheritdoc />
    public partial class dELETEuRSE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Account",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "CreatedByName",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "DeleteById",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "DeleteByName",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "LoginTime",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Sys_User");

            migrationBuilder.DropColumn(
                name: "UpdatedByName",
                table: "Sys_User");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Account",
                table: "Sys_User",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "Sys_User",
                type: "nvarchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "Sys_User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Sys_User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "CreatedByName",
                table: "Sys_User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "DeleteById",
                table: "Sys_User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DeleteByName",
                table: "Sys_User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Sys_User",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Sys_User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Sys_User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LoginTime",
                table: "Sys_User",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Sys_User",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Sys_User",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "UpdatedById",
                table: "Sys_User",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedByName",
                table: "Sys_User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
