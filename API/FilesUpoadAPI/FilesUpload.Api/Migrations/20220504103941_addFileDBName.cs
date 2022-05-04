using Microsoft.EntityFrameworkCore.Migrations;

namespace FilesUpload.Api.Migrations
{
    public partial class addFileDBName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileDBName",
                table: "files",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileDBName",
                table: "files");
        }
    }
}
