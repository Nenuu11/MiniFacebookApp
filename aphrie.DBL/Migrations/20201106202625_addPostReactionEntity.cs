using Microsoft.EntityFrameworkCore.Migrations;

namespace aphrie.DBL.Migrations
{
    public partial class addPostReactionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reactions",
                table: "Posts");

            migrationBuilder.CreateTable(
                name: "PostReaction",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    PostId = table.Column<string>(nullable: true),
                    Reactions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostReaction_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostReaction_PostId",
                table: "PostReaction",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostReaction");

            migrationBuilder.AddColumn<int>(
                name: "Reactions",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
