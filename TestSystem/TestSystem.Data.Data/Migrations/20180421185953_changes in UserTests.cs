using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TestSystem.Data.Data.Migrations
{
    public partial class changesinUserTests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Passed",
                table: "UserTests");

            migrationBuilder.AddColumn<DateTime>(
                name: "SubmittedOn",
                table: "UserTests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubmittedOn",
                table: "UserTests");

            migrationBuilder.AddColumn<bool>(
                name: "Passed",
                table: "UserTests",
                nullable: true);
        }
    }
}
