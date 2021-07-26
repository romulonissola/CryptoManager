using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptoManager.Repository.Migrations
{
    public partial class dotnet3migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExchangeType",
                table: "Exchange",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExchangeType",
                table: "Exchange");
        }
    }
}
