using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class tbl_PessoaCliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PessoasClientesVinculadas",
                columns: table => new
                {
                    PessoaClienteEntityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioVinculadoId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    AcessoPermitido = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoasClientesVinculadas", x => new { x.PessoaClienteEntityId, x.UsuarioVinculadoId });
                    table.ForeignKey(
                        name: "FK_PessoasClientesVinculadas_AspNetUsers_UsuarioVinculadoId",
                        column: x => x.UsuarioVinculadoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PessoasClientesVinculadas_PessoaClientes_PessoaClienteEntity~",
                        column: x => x.PessoaClienteEntityId,
                        principalTable: "PessoaClientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PessoasClientesVinculadas_UsuarioVinculadoId",
                table: "PessoasClientesVinculadas",
                column: "UsuarioVinculadoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoasClientesVinculadas");
        }
    }
}
