using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchMeUp.Core.Migrations
{
    public partial class AddTeam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MemberType",
                table: "Members",
                newName: "TeamId");

            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Followers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "MemberInterests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestId = table.Column<int>(type: "int", nullable: false),
                    MemberId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberInterests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamEvents_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Members_TeamId",
                table: "Members",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TeamEvents_TeamId",
                table: "TeamEvents",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Teams_TeamId",
                table: "Members",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Teams_TeamId",
                table: "Members");

            migrationBuilder.DropTable(
                name: "MemberInterests");

            migrationBuilder.DropTable(
                name: "TeamEvents");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Members_TeamId",
                table: "Members");

            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Followers");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "Members",
                newName: "MemberType");
        }
    }
}
