using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEnd.DAL.Migrations
{
    public partial class updateHomePageV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_JobQuestions_JobQuestionsId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobQuestionsId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CompanyName",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobQuestionsId",
                table: "Jobs");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Jobs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "jobId",
                table: "JobQuestions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_JobQuestions_jobId",
                table: "JobQuestions",
                column: "jobId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobQuestions_Jobs_jobId",
                table: "JobQuestions",
                column: "jobId",
                principalTable: "Jobs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_Company_CompanyId",
                table: "Jobs",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobQuestions_Jobs_jobId",
                table: "JobQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_Company_CompanyId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_CompanyId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_JobQuestions_jobId",
                table: "JobQuestions");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "jobId",
                table: "JobQuestions");

            migrationBuilder.AddColumn<string>(
                name: "CompanyName",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobQuestionsId",
                table: "Jobs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobQuestionsId",
                table: "Jobs",
                column: "JobQuestionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_JobQuestions_JobQuestionsId",
                table: "Jobs",
                column: "JobQuestionsId",
                principalTable: "JobQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
