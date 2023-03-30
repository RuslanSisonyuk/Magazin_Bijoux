    using Microsoft.EntityFrameworkCore.Migrations;

namespace Magazin_Bijoux.Data.Migrations
{
    public partial class initproducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    details = table.Column<string>(nullable: false),
                    size = table.Column<string>(nullable: false),
                    color = table.Column<string>(nullable: false),
                    price = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}
