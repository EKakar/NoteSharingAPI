using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class sp3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserLists");

            migrationBuilder.Sql($@"
                    Create proc sp_getNoteById
                    (
	                    @id int
                    )
                    as
                    select n.Description [Title], c.CategoryName [Category] from Notes n 
					join Categories c on n.CategoryID = c.CategoryID
					where n.NoteId = @id
                    ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserLists",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.Sql($@"DROP PROC sp_getNoteById");

        }
    }
}
