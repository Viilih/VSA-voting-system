using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace votingSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixVoteRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                column: "CandidateId",
                unique: true);
        }
    }
}
