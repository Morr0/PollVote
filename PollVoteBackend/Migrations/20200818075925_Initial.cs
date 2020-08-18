using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PollVoteBackend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poll",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    DeleteToken = table.Column<string>(nullable: true),
                    Question = table.Column<string>(nullable: false),
                    Choices = table.Column<List<string>>(nullable: false),
                    ChoicesAnswers = table.Column<List<int>>(nullable: true),
                    ExpiresOnChoices = table.Column<int>(nullable: false),
                    EndedOn = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poll", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Poll");
        }
    }
}
