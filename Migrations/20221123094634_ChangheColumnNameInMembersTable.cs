using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Maier_Teodora_Lab2.Migrations
{
    public partial class ChangheColumnNameInMembersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Member",
                newName: "Address");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Member",
                newName: "Adress");
        }
    }
}
