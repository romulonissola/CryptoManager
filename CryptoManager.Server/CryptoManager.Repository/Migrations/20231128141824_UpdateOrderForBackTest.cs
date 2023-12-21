using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoManager.Repository.Migrations
{
    public partial class UpdateOrderForBackTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsBackTest",
                table: "Order",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBackTest",
                table: "Order");
        }
    }
}
