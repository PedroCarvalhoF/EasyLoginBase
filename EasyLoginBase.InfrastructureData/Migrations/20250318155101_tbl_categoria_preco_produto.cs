using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class tbl_categoria_preco_produto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasPrecosProdutos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    CategoriaPreco = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClienteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioRegistroId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Habilitado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasPrecosProdutos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PrecoProdutoEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ProdutoEntityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    FilialEntityId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    PrecoProduto = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    TipoPrecoProdutoEnum = table.Column<int>(type: "int", nullable: false),
                    ClienteId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsuarioRegistroId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Habilitado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrecoProdutoEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrecoProdutoEntity_Filiais_FilialEntityId",
                        column: x => x.FilialEntityId,
                        principalTable: "Filiais",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrecoProdutoEntity_Produtos_ProdutoEntityId",
                        column: x => x.ProdutoEntityId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasPrecosProdutos_CategoriaPreco",
                table: "CategoriasPrecosProdutos",
                column: "CategoriaPreco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PrecoProdutoEntity_FilialEntityId",
                table: "PrecoProdutoEntity",
                column: "FilialEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_PrecoProdutoEntity_ProdutoEntityId",
                table: "PrecoProdutoEntity",
                column: "ProdutoEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriasPrecosProdutos");

            migrationBuilder.DropTable(
                name: "PrecoProdutoEntity");
        }
    }
}
