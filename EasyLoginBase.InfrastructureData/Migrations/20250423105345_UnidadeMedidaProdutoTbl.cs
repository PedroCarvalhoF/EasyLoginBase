using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class UnidadeMedidaProdutoTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UnidadeMedidaProdutoEntityId",
                table: "Produtos",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateTable(
                name: "UnidadeMedidaProdutos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Nome = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Sigla = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadeMedidaProdutos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_UnidadeMedidaProdutoEntityId",
                table: "Produtos",
                column: "UnidadeMedidaProdutoEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_UnidadeMedidaProdutos_UnidadeMedidaProdutoEntityId",
                table: "Produtos",
                column: "UnidadeMedidaProdutoEntityId",
                principalTable: "UnidadeMedidaProdutos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_UnidadeMedidaProdutos_UnidadeMedidaProdutoEntityId",
                table: "Produtos");

            migrationBuilder.DropTable(
                name: "UnidadeMedidaProdutos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_UnidadeMedidaProdutoEntityId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "UnidadeMedidaProdutoEntityId",
                table: "Produtos");
        }
    }
}
