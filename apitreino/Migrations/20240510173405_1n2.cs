﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apitreino.Migrations
{
    /// <inheritdoc />
    public partial class _1n2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoass",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Idade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoass", x => x.Id);
                });

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
            migrationBuilder.DropTable(
                name: "Pessoass");

            migrationBuilder.DropTable(
                name: "Enderecos");

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
