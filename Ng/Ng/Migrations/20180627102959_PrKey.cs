using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Ng.Migrations
{
    public partial class PrKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Tours",
                table: "Tours");

            migrationBuilder.DropIndex(
                name: "IX_Tours_ExcursionId",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Tours");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tours",
                table: "Tours",
                columns: new[] { "ExcursionId", "ClientId" });
        }
    }
}
