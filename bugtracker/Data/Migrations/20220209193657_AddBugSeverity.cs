using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bugtracker.Data.Migrations
{
    public partial class AddBugSeverity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "severity",
                table: "BugDatabase",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "severity",
                table: "BugDatabase");
        }
    }
}
