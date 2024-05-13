using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apitreino.Migrations
{
    /// <inheritdoc />
    public partial class _1n : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoass_Enderecos_EnderecoId",
                table: "Pessoass");

            migrationBuilder.DropIndex(
                name: "IX_Pessoass_EnderecoId",
                table: "Pessoass");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Pessoass");

            migrationBuilder.AddColumn<int>(
                name: "PessoasId",
                table: "Enderecos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Enderecos_PessoasId",
                table: "Enderecos",
                column: "PessoasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Enderecos_Pessoass_PessoasId",
                table: "Enderecos",
                column: "PessoasId",
                principalTable: "Pessoass",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enderecos_Pessoass_PessoasId",
                table: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Enderecos_PessoasId",
                table: "Enderecos");

            migrationBuilder.DropColumn(
                name: "PessoasId",
                table: "Enderecos");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Pessoass",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoass_EnderecoId",
                table: "Pessoass",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pessoass_Enderecos_EnderecoId",
                table: "Pessoass",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
