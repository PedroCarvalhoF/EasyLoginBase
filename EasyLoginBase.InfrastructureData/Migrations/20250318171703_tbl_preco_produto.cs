using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class tbl_preco_produto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrecoProdutoEntity_Filiais_FilialEntityId",
                table: "PrecoProdutoEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecoProdutoEntity_Produtos_ProdutoEntityId",
                table: "PrecoProdutoEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrecoProdutoEntity",
                table: "PrecoProdutoEntity");

            migrationBuilder.DropIndex(
                name: "IX_PrecoProdutoEntity_ProdutoEntityId",
                table: "PrecoProdutoEntity");

            migrationBuilder.RenameTable(
                name: "PrecoProdutoEntity",
                newName: "PrecosProdutos");

            migrationBuilder.RenameIndex(
                name: "IX_PrecoProdutoEntity_FilialEntityId",
                table: "PrecosProdutos",
                newName: "IX_PrecosProdutos_FilialEntityId");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoProduto",
                table: "PrecosProdutos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoriaPrecoProdutoEntityId",
                table: "PrecosProdutos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrecosProdutos",
                table: "PrecosProdutos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrecosProdutos_CategoriaPrecoProdutoEntityId",
                table: "PrecosProdutos",
                column: "CategoriaPrecoProdutoEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_Filial_Categoria",
                table: "PrecosProdutos",
                columns: new[] { "ProdutoEntityId", "FilialEntityId", "CategoriaPrecoProdutoEntityId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PrecosProdutos_CategoriasPrecosProdutos_CategoriaPrecoProdut~",
                table: "PrecosProdutos",
                column: "CategoriaPrecoProdutoEntityId",
                principalTable: "CategoriasPrecosProdutos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecosProdutos_Filiais_FilialEntityId",
                table: "PrecosProdutos",
                column: "FilialEntityId",
                principalTable: "Filiais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecosProdutos_Produtos_ProdutoEntityId",
                table: "PrecosProdutos",
                column: "ProdutoEntityId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrecosProdutos_CategoriasPrecosProdutos_CategoriaPrecoProdut~",
                table: "PrecosProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecosProdutos_Filiais_FilialEntityId",
                table: "PrecosProdutos");

            migrationBuilder.DropForeignKey(
                name: "FK_PrecosProdutos_Produtos_ProdutoEntityId",
                table: "PrecosProdutos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PrecosProdutos",
                table: "PrecosProdutos");

            migrationBuilder.DropIndex(
                name: "IX_PrecosProdutos_CategoriaPrecoProdutoEntityId",
                table: "PrecosProdutos");

            migrationBuilder.DropIndex(
                name: "IX_Produto_Filial_Categoria",
                table: "PrecosProdutos");

            migrationBuilder.DropColumn(
                name: "CategoriaPrecoProdutoEntityId",
                table: "PrecosProdutos");

            migrationBuilder.RenameTable(
                name: "PrecosProdutos",
                newName: "PrecoProdutoEntity");

            migrationBuilder.RenameIndex(
                name: "IX_PrecosProdutos_FilialEntityId",
                table: "PrecoProdutoEntity",
                newName: "IX_PrecoProdutoEntity_FilialEntityId");

            migrationBuilder.AlterColumn<decimal>(
                name: "PrecoProduto",
                table: "PrecoProdutoEntity",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PrecoProdutoEntity",
                table: "PrecoProdutoEntity",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PrecoProdutoEntity_ProdutoEntityId",
                table: "PrecoProdutoEntity",
                column: "ProdutoEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PrecoProdutoEntity_Filiais_FilialEntityId",
                table: "PrecoProdutoEntity",
                column: "FilialEntityId",
                principalTable: "Filiais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PrecoProdutoEntity_Produtos_ProdutoEntityId",
                table: "PrecoProdutoEntity",
                column: "ProdutoEntityId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
