using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class tbl_produto_alter_campos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtos_CodigoProduto",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_NomeProduto",
                table: "Produtos");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaProdutoEntityId",
                table: "Produtos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)",
                oldNullable: true)
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CodigoProduto",
                table: "Produtos",
                column: "CodigoProduto",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_NomeProduto",
                table: "Produtos",
                column: "NomeProduto",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Produtos_CodigoProduto",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_NomeProduto",
                table: "Produtos");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoriaProdutoEntityId",
                table: "Produtos",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci",
                oldClrType: typeof(Guid),
                oldType: "char(36)")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CodigoProduto",
                table: "Produtos",
                column: "CodigoProduto");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_NomeProduto",
                table: "Produtos",
                column: "NomeProduto");
        }
    }
}
