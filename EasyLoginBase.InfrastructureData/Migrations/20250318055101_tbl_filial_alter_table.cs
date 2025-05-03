using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EasyLoginBase.InfrastructureData.Migrations
{
    /// <inheritdoc />
    public partial class tbl_filial_alter_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCriacaoFilial",
                table: "Filiais");

            migrationBuilder.RenameColumn(
                name: "Habilitada",
                table: "Filiais",
                newName: "Habilitado");

            migrationBuilder.AddColumn<Guid>(
                name: "ClienteId",
                table: "Filiais",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "Filiais",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "Filiais",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioRegistroId",
                table: "Filiais",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Filiais_NomeFilial",
                table: "Filiais",
                column: "NomeFilial",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Filiais_NomeFilial",
                table: "Filiais");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Filiais");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "Filiais");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "Filiais");

            migrationBuilder.DropColumn(
                name: "UsuarioRegistroId",
                table: "Filiais");

            migrationBuilder.RenameColumn(
                name: "Habilitado",
                table: "Filiais",
                newName: "Habilitada");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacaoFilial",
                table: "Filiais",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
