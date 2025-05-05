using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class tbl_categoria_produto_not_is_unique_nomeCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CategoriasProdutos_NomeCategoria",
                table: "CategoriasProdutos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CategoriasProdutos_NomeCategoria",
                table: "CategoriasProdutos",
                column: "NomeCategoria",
                unique: true);
        }
    }
}
