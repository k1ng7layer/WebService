using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccesLayer.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
            name: "Quantity",
            table: "Carts",
            type: "int",
            nullable: false,
            defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
          
        }
    }
}
