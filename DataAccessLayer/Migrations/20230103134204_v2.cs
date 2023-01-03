using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileDetails",
                table: "FileDetails");

            migrationBuilder.RenameTable(
                name: "FileDetails",
                newName: "NotesFiles");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "NotesFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "NotesFiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoteLevel",
                table: "NotesFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoteRef",
                table: "NotesFiles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RatingScore",
                table: "NotesFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "NotesFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_NotesFiles",
                table: "NotesFiles",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_NotesFiles_CategoryID",
                table: "NotesFiles",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_NotesFiles_UserId",
                table: "NotesFiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesFiles_Categories_CategoryID",
                table: "NotesFiles",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_NotesFiles_Users_UserId",
                table: "NotesFiles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotesFiles_Categories_CategoryID",
                table: "NotesFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_NotesFiles_Users_UserId",
                table: "NotesFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NotesFiles",
                table: "NotesFiles");

            migrationBuilder.DropIndex(
                name: "IX_NotesFiles_CategoryID",
                table: "NotesFiles");

            migrationBuilder.DropIndex(
                name: "IX_NotesFiles_UserId",
                table: "NotesFiles");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "NotesFiles");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "NotesFiles");

            migrationBuilder.DropColumn(
                name: "NoteLevel",
                table: "NotesFiles");

            migrationBuilder.DropColumn(
                name: "NoteRef",
                table: "NotesFiles");

            migrationBuilder.DropColumn(
                name: "RatingScore",
                table: "NotesFiles");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NotesFiles");

            migrationBuilder.RenameTable(
                name: "NotesFiles",
                newName: "FileDetails");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileDetails",
                table: "FileDetails",
                column: "ID");

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoteLevel = table.Column<int>(type: "int", nullable: false),
                    RatingScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_Notes_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "CategoryID");
                    table.ForeignKey(
                        name: "FK_Notes_FileDetails_NoteId",
                        column: x => x.NoteId,
                        principalTable: "FileDetails",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notes_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_CategoryID",
                table: "Notes",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Notes_UserId",
                table: "Notes",
                column: "UserId");
        }
    }
}
