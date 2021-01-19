using Microsoft.EntityFrameworkCore.Migrations;

namespace botClientApi.Migrations
{
    public partial class Init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Telegramuserid",
                table: "messages",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Telegramuserid",
                table: "messages");
        }
    }
}
