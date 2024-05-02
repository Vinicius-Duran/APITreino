using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apitreino.Migrations
{
    /// <inheritdoc />
    public partial class aab : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Pessoass",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cep = table.Column<int>(type: "int", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    numero = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pessoass_Enderecos_EnderecoId",
                table: "Pessoass");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Pessoass_EnderecoId",
                table: "Pessoass");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Pessoass");
        }
    }
}
