using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PushNotification.Migrations
{
    public partial class WidthHeightScreen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nome = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Inscricao",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    aparelho = table.Column<Guid>(type: "uuid", nullable: false),
                    endpoint = table.Column<string>(type: "text", nullable: false),
                    p26dh = table.Column<string>(type: "text", nullable: false),
                    auth = table.Column<string>(type: "text", nullable: false),
                    namePlatform = table.Column<string>(type: "text", nullable: true),
                    versionPlatform = table.Column<string>(type: "text", nullable: true),
                    layoutPlatform = table.Column<string>(type: "text", nullable: true),
                    preleasePlatform = table.Column<string>(type: "text", nullable: true),
                    osPlatform = table.Column<string>(type: "text", nullable: true),
                    manufacturerPlatform = table.Column<string>(type: "text", nullable: true),
                    productPlatform = table.Column<string>(type: "text", nullable: true),
                    descriptionPlatform = table.Column<string>(type: "text", nullable: true),
                    uaPlatform = table.Column<string>(type: "text", nullable: true),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    widthScreen = table.Column<string>(type: "text", nullable: true),
                    heightScreen = table.Column<string>(type: "text", nullable: true),
                    usuario_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inscricao", x => x.id);
                    table.ForeignKey(
                        name: "FK_Inscricao_Usuario_usuario_id",
                        column: x => x.usuario_id,
                        principalTable: "Usuario",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_endpoint",
                table: "Inscricao",
                column: "endpoint",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inscricao_usuario_id",
                table: "Inscricao",
                column: "usuario_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Inscricao");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
