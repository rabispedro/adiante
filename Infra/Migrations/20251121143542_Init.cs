using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Antecipacoes",
                columns: table => new
                {
                    Cnpj = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Empresa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Limite = table.Column<decimal>(type: "decimal(10,10)", nullable: false),
                    TotalBruto = table.Column<decimal>(type: "decimal(10,10)", nullable: false),
                    TotalLiquido = table.Column<decimal>(type: "decimal(10,10)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antecipacoes", x => x.Cnpj);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Cnpj = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Faturamento = table.Column<decimal>(type: "decimal(10,10)", nullable: false),
                    Ramo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Cnpj);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Antecipacoes");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
