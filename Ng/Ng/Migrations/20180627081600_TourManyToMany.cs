using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ng.Migrations
{
    public partial class TourManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Clients_ClientId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Excursions_ExcursionId",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_ExcursionId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "ExcursionId",
                table: "Tours",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Tours",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tours",
                table: "Tours",
                columns: new[] { "ExcursionId", "ClientId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Clients_ClientId",
                table: "Tours",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Excursions_ExcursionId",
                table: "Tours",
                column: "ExcursionId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Clients_ClientId",
                table: "Tours");

            migrationBuilder.DropForeignKey(
                name: "FK_Tours_Excursions_ExcursionId",
                table: "Tours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

            migrationBuilder.AlterColumn<int>(
                name: "ClientId",
                table: "Tours",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ExcursionId",
                table: "Tours",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Tours",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tours",
                table: "Tours",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tours_ExcursionId",
                table: "Tours",
                column: "ExcursionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Clients_ClientId",
                table: "Tours",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tours_Excursions_ExcursionId",
                table: "Tours",
                column: "ExcursionId",
                principalTable: "Excursions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
