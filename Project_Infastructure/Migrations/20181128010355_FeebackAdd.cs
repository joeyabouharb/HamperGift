using Microsoft.EntityFrameworkCore.Migrations;

namespace Project_Infastructure.Migrations
{
    public partial class FeebackAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Feedback",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Feedback");
        }
    }
}
