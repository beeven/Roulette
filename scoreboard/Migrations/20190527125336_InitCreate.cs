using Microsoft.EntityFrameworkCore.Migrations;

namespace Scoreboard.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: false),
                    Score = table.Column<string>(nullable: true),
                    Score1 = table.Column<string>(nullable: true),
                    Score2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Number);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidates");
        }
    }
}
