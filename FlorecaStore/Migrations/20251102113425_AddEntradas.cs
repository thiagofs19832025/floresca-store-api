using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlorecaStore.Migrations
{
    public partial class AddEntradas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrada_Produtos_ProdutoId",
                table: "Entrada");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entrada",
                table: "Entrada");

            migrationBuilder.RenameTable(
                name: "Entrada",
                newName: "Entradas");

            migrationBuilder.RenameIndex(
                name: "IX_Entrada_ProdutoId",
                table: "Entradas",
                newName: "IX_Entradas_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entradas",
                table: "Entradas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entradas_Produtos_ProdutoId",
                table: "Entradas",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entradas_Produtos_ProdutoId",
                table: "Entradas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Entradas",
                table: "Entradas");

            migrationBuilder.RenameTable(
                name: "Entradas",
                newName: "Entrada");

            migrationBuilder.RenameIndex(
                name: "IX_Entradas_ProdutoId",
                table: "Entrada",
                newName: "IX_Entrada_ProdutoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Entrada",
                table: "Entrada",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrada_Produtos_ProdutoId",
                table: "Entrada",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
