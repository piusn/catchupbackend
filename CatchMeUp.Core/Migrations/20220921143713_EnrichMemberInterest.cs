using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatchMeUp.Core.Migrations
{
    public partial class EnrichMemberInterest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MemberInterests_InterestId",
                table: "MemberInterests",
                column: "InterestId");

            migrationBuilder.CreateIndex(
                name: "IX_MemberInterests_MemberId",
                table: "MemberInterests",
                column: "MemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_MemberInterests_Interests_InterestId",
                table: "MemberInterests",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MemberInterests_Members_MemberId",
                table: "MemberInterests",
                column: "MemberId",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MemberInterests_Interests_InterestId",
                table: "MemberInterests");

            migrationBuilder.DropForeignKey(
                name: "FK_MemberInterests_Members_MemberId",
                table: "MemberInterests");

            migrationBuilder.DropIndex(
                name: "IX_MemberInterests_InterestId",
                table: "MemberInterests");

            migrationBuilder.DropIndex(
                name: "IX_MemberInterests_MemberId",
                table: "MemberInterests");
        }
    }
}
