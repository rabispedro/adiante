using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddNotasFiscais : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotasFiscais",
                columns: table => new
                {
                    Numero = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpresaCnpj = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotasFiscais", x => x.Numero);
                    table.ForeignKey(
                        name: "FK_NotasFiscais_Empresas_EmpresaCnpj",
                        column: x => x.EmpresaCnpj,
                        principalTable: "Empresas",
                        principalColumn: "Cnpj");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotasFiscais_EmpresaCnpj",
                table: "NotasFiscais",
                column: "EmpresaCnpj");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotasFiscais");
        }
    }
}
