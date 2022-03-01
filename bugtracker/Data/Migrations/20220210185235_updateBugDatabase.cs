using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bugtracker.Data.Migrations
{
    public partial class updateBugDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "severity",
                table: "BugDatabase",
                newName: "Severity");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "BugDatabase",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "BugDatabase",
                newName: "Description");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "BugDatabase",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "BugDatabase",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "BugDatabase",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "BugDatabase");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "BugDatabase");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BugDatabase");

            migrationBuilder.RenameColumn(
                name: "Severity",
                table: "BugDatabase",
                newName: "severity");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "BugDatabase",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "BugDatabase",
                newName: "description");
        }
    }
}
