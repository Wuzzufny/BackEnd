using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_CompanySize_CompanySizaID",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Country_CountryID",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_CompanyIndustry_IndustryID",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_UserID",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_UserID",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "Employee",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Employee",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Country",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CompanySize",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "CompanyIndustry",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IndustryID",
                table: "Client",
                newName: "IndustryId");

            migrationBuilder.RenameColumn(
                name: "CountryID",
                table: "Client",
                newName: "CountryId");

            migrationBuilder.RenameColumn(
                name: "CompanySizaID",
                table: "Client",
                newName: "CompanySizaId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Client",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Client_IndustryID",
                table: "Client",
                newName: "IX_Client_IndustryId");

            migrationBuilder.RenameIndex(
                name: "IX_Client_CountryID",
                table: "Client",
                newName: "IX_Client_CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_Client_CompanySizaID",
                table: "Client",
                newName: "IX_Client_CompanySizaId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserId",
                table: "Employee",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_CompanySize_CompanySizaId",
                table: "Client",
                column: "CompanySizaId",
                principalTable: "CompanySize",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Country_CountryId",
                table: "Client",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_CompanyIndustry_IndustryId",
                table: "Client",
                column: "IndustryId",
                principalTable: "CompanyIndustry",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_UserId",
                table: "Employee",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_CompanySize_CompanySizaId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_Country_CountryId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Client_CompanyIndustry_IndustryId",
                table: "Client");

            migrationBuilder.DropForeignKey(
                name: "FK_Employee_AspNetUsers_UserId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_UserId",
                table: "Employee");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Employee",
                newName: "UserID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Employee",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Country",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CompanySize",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CompanyIndustry",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "IndustryId",
                table: "Client",
                newName: "IndustryID");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "Client",
                newName: "CountryID");

            migrationBuilder.RenameColumn(
                name: "CompanySizaId",
                table: "Client",
                newName: "CompanySizaID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Client",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Client_IndustryId",
                table: "Client",
                newName: "IX_Client_IndustryID");

            migrationBuilder.RenameIndex(
                name: "IX_Client_CountryId",
                table: "Client",
                newName: "IX_Client_CountryID");

            migrationBuilder.RenameIndex(
                name: "IX_Client_CompanySizaId",
                table: "Client",
                newName: "IX_Client_CompanySizaID");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_UserID",
                table: "Employee",
                column: "UserID",
                unique: true,
                filter: "[UserID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_CompanySize_CompanySizaID",
                table: "Client",
                column: "CompanySizaID",
                principalTable: "CompanySize",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Country_CountryID",
                table: "Client",
                column: "CountryID",
                principalTable: "Country",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Client_CompanyIndustry_IndustryID",
                table: "Client",
                column: "IndustryID",
                principalTable: "CompanyIndustry",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_AspNetUsers_UserID",
                table: "Employee",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
