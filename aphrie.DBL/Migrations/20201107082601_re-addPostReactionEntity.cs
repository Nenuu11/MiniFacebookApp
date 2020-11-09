using Microsoft.EntityFrameworkCore.Migrations;

namespace aphrie.DBL.Migrations
{
    public partial class readdPostReactionEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostReaction",
                columns: table => new
                {
                    PostId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Reactions = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReaction", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostReaction_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReaction_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
        }
    }
}
