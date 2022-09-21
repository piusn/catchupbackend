using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchMeUp.Core.Migrations
{
    public partial class AddAvailabiltyCollection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailabilityId",
                table: "MemberInterests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MemberAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberAvailability", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberInterests_AvailabilityId",
                table: "MemberInterests",
                column: "AvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberInterests_MemberAvailability_AvailabilityId",
                table: "MemberInterests",
                column: "AvailabilityId",
                principalTable: "MemberAvailability",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberInterests_MemberAvailability_AvailabilityId",
                table: "MemberInterests");

            migrationBuilder.DropTable(
                name: "MemberAvailability");

            migrationBuilder.DropIndex(
                name: "IX_MemberInterests_AvailabilityId",
                table: "MemberInterests");

            migrationBuilder.DropColumn(
                name: "AvailabilityId",
                table: "MemberInterests");
        }
    }
}
