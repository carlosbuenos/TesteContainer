using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Pessoas.Migrations
{
    public partial class intToStringNumber : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Numero",
                table: "PessoaTelefone",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "DDD",
                table: "PessoaTelefone",
                nullable: false,
                oldClrType: typeof(int));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Numero",
                table: "PessoaTelefone",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "DDD",
                table: "PessoaTelefone",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
