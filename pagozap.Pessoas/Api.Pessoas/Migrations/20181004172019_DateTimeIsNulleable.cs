using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Pessoas.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class DateTimeIsNulleable : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "PessoaFisica",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataNascimento",
                table: "PessoaFisica",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
