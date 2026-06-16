using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GemMangement.DAL.Migrations
{
    /// <inheritdoc />
    public partial class creat2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "memberShips",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "members",
                newName: "JoinDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDate",
                table: "memberShips",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "memberShips",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "JoinDate",
                table: "members",
                newName: "StartDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "memberShips",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
