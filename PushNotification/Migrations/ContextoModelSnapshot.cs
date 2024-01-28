﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PushNotification.Configuracao;

#nullable disable

namespace PushNotification.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PushNotification.Models.Inscricao", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<Guid>("aparelho")
                        .HasColumnType("uuid")
                        .HasColumnName("aparelho");

                    b.Property<string>("auth")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("auth");

                    b.Property<DateTime?>("createdAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("createdAt");

                    b.Property<string>("descriptionPlatform")
                        .HasColumnType("text")
                        .HasColumnName("descriptionPlatform");

                    b.Property<string>("endpoint")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("endpoint");

                    b.Property<string>("heightScreen")
                        .HasColumnType("text")
                        .HasColumnName("heightScreen");

                    b.Property<string>("layoutPlatform")
                        .HasColumnType("text")
                        .HasColumnName("layoutPlatform");

                    b.Property<string>("manufacturerPlatform")
                        .HasColumnType("text")
                        .HasColumnName("manufacturerPlatform");

                    b.Property<string>("namePlatform")
                        .HasColumnType("text")
                        .HasColumnName("namePlatform");

                    b.Property<string>("osPlatform")
                        .HasColumnType("text")
                        .HasColumnName("osPlatform");

                    b.Property<string>("p26dh")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("p26dh");

                    b.Property<string>("preleasePlatform")
                        .HasColumnType("text")
                        .HasColumnName("preleasePlatform");

                    b.Property<string>("productPlatform")
                        .HasColumnType("text")
                        .HasColumnName("productPlatform");

                    b.Property<string>("uaPlatform")
                        .HasColumnType("text")
                        .HasColumnName("uaPlatform");

                    b.Property<DateTime?>("updatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updatedAt");

                    b.Property<int?>("usuario_id")
                        .HasColumnType("integer")
                        .HasColumnName("usuario_id");

                    b.Property<string>("versionPlatform")
                        .HasColumnType("text")
                        .HasColumnName("versionPlatform");

                    b.Property<string>("widthScreen")
                        .HasColumnType("text")
                        .HasColumnName("widthScreen");

                    b.HasKey("id");

                    b.HasIndex("endpoint")
                        .IsUnique();

                    b.HasIndex("usuario_id");

                    b.ToTable("Inscricao");
                });

            modelBuilder.Entity("PushNotification.Models.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("nome")
                        .HasColumnType("text")
                        .HasColumnName("nome");

                    b.HasKey("id");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("PushNotification.Models.Inscricao", b =>
                {
                    b.HasOne("PushNotification.Models.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("usuario_id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Usuario");
                });
#pragma warning restore 612, 618
        }
    }
}
