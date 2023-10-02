using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AWSTranslate.API.Migrations
{
    public partial class Add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnglishTranslatedWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnglishTranslatedWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HindiTranslatedWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HindiTranslatedWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarathiTranslatedWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarathiTranslatedWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TamilTranslatedWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TamilTranslatedWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeluguTranslatedWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeluguTranslatedWords", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnglishTranslatedWords");

            migrationBuilder.DropTable(
                name: "HindiTranslatedWords");

            migrationBuilder.DropTable(
                name: "MarathiTranslatedWords");

            migrationBuilder.DropTable(
                name: "TamilTranslatedWords");

            migrationBuilder.DropTable(
                name: "TeluguTranslatedWords");
        }
    }
}
