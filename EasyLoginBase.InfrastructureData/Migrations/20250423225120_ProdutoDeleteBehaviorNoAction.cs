using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class ProdutoDeleteBehaviorNoAction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_CategoriasProdutos_CategoriaProdutoEntityId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_UnidadeMedidaProdutos_UnidadeMedidaProdutoEntityId",
                table: "Produtos");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_CategoriasProdutos_CategoriaProdutoEntityId",
                table: "Produtos",
                column: "CategoriaProdutoEntityId",
                principalTable: "CategoriasProdutos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_UnidadeMedidaProdutos_UnidadeMedidaProdutoEntityId",
                table: "Produtos",
                column: "UnidadeMedidaProdutoEntityId",
                principalTable: "UnidadeMedidaProdutos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_CategoriasProdutos_CategoriaProdutoEntityId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_UnidadeMedidaProdutos_UnidadeMedidaProdutoEntityId",
                table: "Produtos");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_CategoriasProdutos_CategoriaProdutoEntityId",
                table: "Produtos",
                column: "CategoriaProdutoEntityId",
                principalTable: "CategoriasProdutos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_UnidadeMedidaProdutos_UnidadeMedidaProdutoEntityId",
                table: "Produtos",
                column: "UnidadeMedidaProdutoEntityId",
                principalTable: "UnidadeMedidaProdutos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
