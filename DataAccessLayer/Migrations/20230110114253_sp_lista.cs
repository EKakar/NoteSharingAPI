using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class sp_lista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql($@"create proc sp_AllCategories as
	                                    select c.CategoryName [Category] from Categories c");

            migrationBuilder.Sql($@"create proc sp_NotesByCategoryID as
                                    	select n.Description [NoteDescription] ,c.CategoryName [Category] from Categories c
                                    	Join Notes n on n.CategoryID = c.CategoryID");

            migrationBuilder.Sql($@"create proc sp_GetUsers as
                                        select u.FirstName [User] from Users u");

            migrationBuilder.CreateTable(
                name: "CategoryLists",
                columns: table => new
                {
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "NoteCategories",
                columns: table => new
                {
                    NoteDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserLists",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryLists");

            migrationBuilder.DropTable(
                name: "NoteCategories");

            migrationBuilder.DropTable(
                name: "UserLists");


            migrationBuilder.Sql($@"DROP PROC sp_AllCategories");
            migrationBuilder.Sql($@"DROP PROC sp_NotesByCategoryID");
            migrationBuilder.Sql($@"DROP PROC sp_GetUsers");
        }
    }
}
