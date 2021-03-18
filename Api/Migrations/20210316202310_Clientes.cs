using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class Clientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "hotsite");

            migrationBuilder.CreateTable(
                name: "cliente",
                schema: "hotsite",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_fantasia = table.Column<string>(maxLength: 250, nullable: true),
                    razao_social = table.Column<string>(maxLength: 250, nullable: true),
                    cnpj = table.Column<string>(maxLength: 18, nullable: true),
                    email = table.Column<string>(maxLength: 30, nullable: true),
                    telefone = table.Column<string>(maxLength: 16, nullable: true),
                    data_cadastro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cliente",
                schema: "hotsite");
        }
    }
}
