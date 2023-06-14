using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForDevs.Infra.Data.Write.Migrations
{
    public partial class AtualizacaoDasTabelas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoNota",
                table: "Avaliacoes");

            migrationBuilder.DropColumn(
                name: "Nota",
                table: "Avaliacoes");

            migrationBuilder.AddColumn<string>(
                name: "MotivoNota",
                table: "AvaliacoesClientes",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Nota",
                table: "AvaliacoesClientes",
                type: "decimal(3,1)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MotivoNota",
                table: "AvaliacoesClientes");

            migrationBuilder.DropColumn(
                name: "Nota",
                table: "AvaliacoesClientes");

            migrationBuilder.AddColumn<string>(
                name: "MotivoNota",
                table: "Avaliacoes",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Nota",
                table: "Avaliacoes",
                type: "decimal(3,1)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
