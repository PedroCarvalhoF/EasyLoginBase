using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class tbl_PontoVenda : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PontosVendas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Aberto = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Cancelado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FilialPdvId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioPdvId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ClienteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioRegistroId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Habilitado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontosVendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PontosVendas_Filiais_FilialPdvId",
                        column: x => x.FilialPdvId,
                        principalTable: "Filiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PontosVendas_UsuariosPdvs_UsuarioPdvId",
                        column: x => x.UsuarioPdvId,
                        principalTable: "UsuariosPdvs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PontosVendas_FilialPdvId",
                table: "PontosVendas",
                column: "FilialPdvId");

            migrationBuilder.CreateIndex(
                name: "IX_PontosVendas_UsuarioPdvId",
                table: "PontosVendas",
                column: "UsuarioPdvId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontosVendas");
        }
    }
}
