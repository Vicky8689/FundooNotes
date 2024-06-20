using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class AddNoteLabelTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "NoteLable",
                columns: table => new
                {
                    NoteLabelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelID = table.Column<int>(nullable: false),
                    NoteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoteLable", x => x.NoteLabelId);
                    table.ForeignKey(
                        name: "FK_NoteLable_Labels_LabelID",
                        column: x => x.LabelID,
                        principalTable: "Labels",
                        principalColumn: "LabelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NoteLable_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NoteLable_LabelID",
                table: "NoteLable",
                column: "LabelID");

            migrationBuilder.CreateIndex(
                name: "IX_NoteLable_NoteId",
                table: "NoteLable",
                column: "NoteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         

            migrationBuilder.CreateTable(
                name: "LabelNote",
                columns: table => new
                {
                    NoteLabelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelID = table.Column<int>(type: "int", nullable: false),
                    NoteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabelNote", x => x.NoteLabelId);
                    table.ForeignKey(
                        name: "FK_LabelNote_Labels_LabelID",
                        column: x => x.LabelID,
                        principalTable: "Labels",
                        principalColumn: "LabelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LabelNote_Notes_NoteId",
                        column: x => x.NoteId,
                        principalTable: "Notes",
                        principalColumn: "NoteId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LabelNote_LabelID",
                table: "LabelNote",
                column: "LabelID");

            migrationBuilder.CreateIndex(
                name: "IX_LabelNote_NoteId",
                table: "LabelNote",
                column: "NoteId");
        }
    }
}
