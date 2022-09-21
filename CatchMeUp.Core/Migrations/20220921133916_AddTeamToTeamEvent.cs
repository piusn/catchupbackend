﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchMeUp.Core.Migrations
{
    public partial class AddTeamToTeamEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamEvents_Teams_TeamId",
                table: "TeamEvents");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "TeamEvents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEvents_Teams_TeamId",
                table: "TeamEvents",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamEvents_Teams_TeamId",
                table: "TeamEvents");

            migrationBuilder.AlterColumn<int>(
                name: "TeamId",
                table: "TeamEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamEvents_Teams_TeamId",
                table: "TeamEvents",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");
        }
    }
}
