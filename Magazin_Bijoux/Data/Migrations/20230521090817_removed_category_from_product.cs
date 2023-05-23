using Microsoft.EntityFrameworkCore.Migrations;

namespace Magazin_Bijoux.Data.Migrations
{
    public partial class removed_category_from_product : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "Product");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "category",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
