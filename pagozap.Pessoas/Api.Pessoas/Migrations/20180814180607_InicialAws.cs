using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Pessoas.Migrations
{
    public partial class InicialAws : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    PessoaID = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.PessoaID);
                });

            migrationBuilder.CreateTable(
                name: "PessoaEmail",
                columns: table => new
                {
                    PessoaEmailID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PessoaID = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FlagPrincipal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaEmail", x => x.PessoaEmailID);
                    table.ForeignKey(
                        name: "FK_PessoaEmail_Pessoas_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaEndereco",
                columns: table => new
                {
                    PessoaEnderecoID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PessoaID = table.Column<string>(nullable: true),
                    UF = table.Column<string>(nullable: true),
                    Municipio = table.Column<string>(nullable: true),
                    Bairro = table.Column<string>(nullable: true),
                    Logradouro = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Complemento = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    FlagPrincipal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaEndereco", x => x.PessoaEnderecoID);
                    table.ForeignKey(
                        name: "FK_PessoaEndereco_Pessoas_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PessoaFisica",
                columns: table => new
                {
                    PessoaID = table.Column<string>(nullable: false),
                    CPF = table.Column<string>(nullable: true),
                    DataNascimento = table.Column<DateTime>(nullable: false),
                    EstadoCivil = table.Column<string>(nullable: true),
                    Sexo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaFisica", x => x.PessoaID);
                    table.ForeignKey(
                        name: "FK_PessoaFisica_Pessoas_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaJuridica",
                columns: table => new
                {
                    PessoaID = table.Column<string>(nullable: false),
                    NomeFantasia = table.Column<string>(nullable: true),
                    CNPJ = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaJuridica", x => x.PessoaID);
                    table.ForeignKey(
                        name: "FK_PessoaJuridica_Pessoas_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PessoaTelefone",
                columns: table => new
                {
                    PessoaID = table.Column<string>(nullable: false),
                    DDD = table.Column<int>(nullable: false),
                    Numero = table.Column<int>(nullable: false),
                    FlagPrincipal = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PessoaTelefone", x => new { x.PessoaID, x.DDD, x.Numero });
                    table.UniqueConstraint("AK_PessoaTelefone_DDD_Numero_PessoaID", x => new { x.DDD, x.Numero, x.PessoaID });
                    table.ForeignKey(
                        name: "FK_PessoaTelefone_Pessoas_PessoaID",
                        column: x => x.PessoaID,
                        principalTable: "Pessoas",
                        principalColumn: "PessoaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PessoaEmail_PessoaID",
                table: "PessoaEmail",
                column: "PessoaID");

            migrationBuilder.CreateIndex(
                name: "IX_PessoaEndereco_PessoaID",
                table: "PessoaEndereco",
                column: "PessoaID");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PessoaEmail");

            migrationBuilder.DropTable(
                name: "PessoaEndereco");

            migrationBuilder.DropTable(
                name: "PessoaFisica");

            migrationBuilder.DropTable(
                name: "PessoaJuridica");

            migrationBuilder.DropTable(
                name: "PessoaTelefone");

            migrationBuilder.DropTable(
                name: "Pessoas");
        }
    }
}
