using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentAutomationLC.Data.Migrations
{
    public partial class ApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentProfiles_PaymentProfileID",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentProfileID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentProfileID",
                table: "Payments");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAdded",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentProfileID",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PaymentProfileID",
                table: "AspNetUsers",
                column: "PaymentProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PaymentProfiles_PaymentProfileID",
                table: "AspNetUsers",
                column: "PaymentProfileID",
                principalTable: "PaymentProfiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PaymentProfiles_PaymentProfileID",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PaymentProfileID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateAdded",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PaymentProfileID",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "PaymentProfileID",
                table: "Payments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_PaymentProfileID",
                table: "Payments",
                column: "PaymentProfileID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PaymentProfiles_PaymentProfileID",
                table: "Payments",
                column: "PaymentProfileID",
                principalTable: "PaymentProfiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
