using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaBoleto.Migrations
{
    public partial class PasswordToCliente : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Clientes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Boletos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Boletos_BancoId",
                table: "Boletos",
                column: "BancoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boletos_Bancos_BancoId",
                table: "Boletos",
                column: "BancoId",
                principalTable: "Bancos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boletos_Bancos_BancoId",
                table: "Boletos");

            migrationBuilder.DropIndex(
                name: "IX_Boletos_BancoId",
                table: "Boletos");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "Clientes");

            migrationBuilder.UpdateData(
                table: "Boletos",
                keyColumn: "Observacao",
                keyValue: null,
                column: "Observacao",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Boletos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
