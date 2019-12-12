﻿// <auto-generated />
using System;
using Api.Pessoas._Migration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Api.Pessoas.Migrations
{
    [DbContext(typeof(MySqlContextUI))]
    partial class MySqlContextUIModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaEmail", b =>
                {
                    b.Property<int>("PessoaEmailID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<bool>("FlagPrincipal");

                    b.Property<string>("PessoaID");

                    b.HasKey("PessoaEmailID");

                    b.HasIndex("PessoaID");

                    b.ToTable("PessoaEmail");
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaEndereco", b =>
                {
                    b.Property<int>("PessoaEnderecoID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Bairro");

                    b.Property<string>("CEP");

                    b.Property<string>("Complemento");

                    b.Property<bool>("FlagPrincipal");

                    b.Property<string>("Logradouro");

                    b.Property<string>("Municipio");

                    b.Property<string>("Numero");

                    b.Property<string>("PessoaID");

                    b.Property<string>("UF");

                    b.HasKey("PessoaEnderecoID");

                    b.HasIndex("PessoaID");

                    b.ToTable("PessoaEndereco");
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaFisica", b =>
                {
                    b.Property<string>("PessoaID");

                    b.Property<string>("CPF");

                    b.Property<DateTime?>("DataNascimento");

                    b.Property<string>("EstadoCivil");

                    b.Property<string>("Sexo");

                    b.HasKey("PessoaID");

                    b.ToTable("PessoaFisica");
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaJuridica", b =>
                {
                    b.Property<string>("PessoaID");

                    b.Property<string>("CNPJ");

                    b.Property<string>("NomeFantasia");

                    b.HasKey("PessoaID");

                    b.ToTable("PessoaJuridica");
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.Pessoas", b =>
                {
                    b.Property<string>("PessoaID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("PessoaID");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaTelefone", b =>
                {
                    b.Property<string>("PessoaID");

                    b.Property<string>("DDD");

                    b.Property<string>("Numero");

                    b.Property<bool>("FlagPrincipal");

                    b.HasKey("PessoaID", "DDD", "Numero");

                    b.HasAlternateKey("DDD", "Numero", "PessoaID");

                    b.ToTable("PessoaTelefone");
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaEmail", b =>
                {
                    b.HasOne("Domain.Pessoas.Entities.Pessoas", "Pessoa")
                        .WithMany("PessoaEmail")
                        .HasForeignKey("PessoaID");
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaEndereco", b =>
                {
                    b.HasOne("Domain.Pessoas.Entities.Pessoas", "Pessoa")
                        .WithMany("PessoaEndereco")
                        .HasForeignKey("PessoaID");
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaFisica", b =>
                {
                    b.HasOne("Domain.Pessoas.Entities.Pessoas", "Pessoa")
                        .WithOne("PessoaFisica")
                        .HasForeignKey("Domain.Pessoas.Entities.PessoaFisica", "PessoaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaJuridica", b =>
                {
                    b.HasOne("Domain.Pessoas.Entities.Pessoas", "Pessoa")
                        .WithOne("PessoaJuridica")
                        .HasForeignKey("Domain.Pessoas.Entities.PessoaJuridica", "PessoaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Domain.Pessoas.Entities.PessoaTelefone", b =>
                {
                    b.HasOne("Domain.Pessoas.Entities.Pessoas", "Pessoa")
                        .WithMany("PessoaTelefone")
                        .HasForeignKey("PessoaID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
