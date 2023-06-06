using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DWShop.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class basketCreation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                schema: "movs",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "movs",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "movs",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "movs",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "LastModifieOn",
                schema: "movs",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                schema: "movs",
                table: "Basket");

            migrationBuilder.AddColumn<int>(
                name: "basketId",
                schema: "movs",
                table: "Basket",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                schema: "movs",
                table: "Basket",
                column: "basketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Basket",
                schema: "movs",
                table: "Basket");

            migrationBuilder.DropColumn(
                name: "basketId",
                schema: "movs",
                table: "Basket");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "movs",
                table: "Basket",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "movs",
                table: "Basket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "movs",
                table: "Basket",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifieOn",
                schema: "movs",
                table: "Basket",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                schema: "movs",
                table: "Basket",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Basket",
                schema: "movs",
                table: "Basket",
                column: "Id");
        }
    }
}
