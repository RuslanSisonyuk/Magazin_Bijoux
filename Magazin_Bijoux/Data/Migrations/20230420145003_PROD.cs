using Microsoft.EntityFrameworkCore.Migrations;

namespace Magazin_Bijoux.Data.Migrations
{
    public partial class PROD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imageURL",
                table: "Product",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imageURL",
                table: "Product");
        }
    }
}
