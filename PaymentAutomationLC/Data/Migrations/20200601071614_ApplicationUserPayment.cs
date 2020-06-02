using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentAutomationLC.Data.Migrations
{
    public partial class ApplicationUserPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ApplicationUserPayments_ApplicationUserPaymentApplicationUserID_ApplicationUserPaymentPaymentID",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_ApplicationUserPayments_ApplicationUserPaymentApplicationUserID1_ApplicationUserPaymentPaymentID1",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ApplicationUserPaymentApplicationUserID_ApplicationUserPaymentPaymentID",
                table: "Articles");

            migrationBuilder.DropIndex(
                name: "IX_Articles_ApplicationUserPaymentApplicationUserID1_ApplicationUserPaymentPaymentID1",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserPaymentApplicationUserID",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserPaymentApplicationUserID1",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserPaymentPaymentID",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserPaymentPaymentID1",
                table: "Articles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserPaymentApplicationUserID",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserPaymentApplicationUserID1",
                table: "Articles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserPaymentPaymentID",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserPaymentPaymentID1",
                table: "Articles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ApplicationUserPaymentApplicationUserID_ApplicationUserPaymentPaymentID",
                table: "Articles",
                columns: new[] { "ApplicationUserPaymentApplicationUserID", "ApplicationUserPaymentPaymentID" });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_ApplicationUserPaymentApplicationUserID1_ApplicationUserPaymentPaymentID1",
                table: "Articles",
                columns: new[] { "ApplicationUserPaymentApplicationUserID1", "ApplicationUserPaymentPaymentID1" });

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ApplicationUserPayments_ApplicationUserPaymentApplicationUserID_ApplicationUserPaymentPaymentID",
                table: "Articles",
                columns: new[] { "ApplicationUserPaymentApplicationUserID", "ApplicationUserPaymentPaymentID" },
                principalTable: "ApplicationUserPayments",
                principalColumns: new[] { "ApplicationUserID", "PaymentID" },
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_ApplicationUserPayments_ApplicationUserPaymentApplicationUserID1_ApplicationUserPaymentPaymentID1",
                table: "Articles",
                columns: new[] { "ApplicationUserPaymentApplicationUserID1", "ApplicationUserPaymentPaymentID1" },
                principalTable: "ApplicationUserPayments",
                principalColumns: new[] { "ApplicationUserID", "PaymentID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
