using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class new_sp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                    create proc sp_DeleteFile
                    (
	                    @id int
                    )
                    as
                    Delete fd from FileDetails fd 
                    where fd.ID= @id
                    ");
            
            migrationBuilder.Sql($@"
                    create proc sp_DeleteNote
                    (
	                    @id int
                    )
                    as
                    Delete n from Notes n 
                    where n.NoteId = @id
                    ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"DROP PROC sp_DeleteFile");
            migrationBuilder.Sql($@"DROP PROC sp_DeleteNote");

        }
    }
}
