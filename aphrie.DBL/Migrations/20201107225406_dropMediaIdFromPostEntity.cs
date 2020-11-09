using Microsoft.EntityFrameworkCore.Migrations;

namespace aphrie.DBL.Migrations
{
    public partial class dropMediaIdFromPostEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaId",
                table: "Posts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaId",
                table: "Posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
