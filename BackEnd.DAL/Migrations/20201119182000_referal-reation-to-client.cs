using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class referalreationtoclient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferalId",
                table: "Client",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Client_ReferalId",
                table: "Client",
                column: "ReferalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Referal_ReferalId",
                table: "Client",
                column: "ReferalId",
                principalTable: "Referal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Referal_ReferalId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_ReferalId",
                table: "Client");

            migrationBuilder.DropColumn(
                name: "ReferalId",
                table: "Client");
        }
    }
}
