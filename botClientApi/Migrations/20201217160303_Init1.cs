using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace botClientApi.Migrations
{
    public partial class Init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chats",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Telegramid = table.Column<long>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Chatid = table.Column<Guid>(nullable: false),
                    Telegramchatid = table.Column<long>(nullable: false),
                    Messageid = table.Column<long>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Firstname = table.Column<string>(nullable: true),
                    Lastname = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chats");

            migrationBuilder.DropTable(
                name: "messages");
        }
    }
}
