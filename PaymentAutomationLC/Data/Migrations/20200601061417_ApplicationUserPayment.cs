using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentAutomationLC.Data.Migrations
{
    public partial class ApplicationUserPayment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserPaymentSummaries");

            migrationBuilder.CreateTable(
                name: "ApplicationUserPayments",
                columns: table => new
                {
                    ApplicationUserID = table.Column<string>(nullable: false),
                    PaymentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserPayments", x => new { x.ApplicationUserID, x.PaymentID });
                    table.ForeignKey(
                        name: "FK_ApplicationUserPayments_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserPayments_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserPayments_PaymentID",
                table: "ApplicationUserPayments",
                column: "PaymentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserPayments");

            migrationBuilder.CreateTable(
                name: "AppUserPaymentSummaries",
                columns: table => new
                {
                    ApplicationUserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserPaymentSummaries", x => new { x.ApplicationUserID, x.PaymentID });
                    table.ForeignKey(
                        name: "FK_AppUserPaymentSummaries_AspNetUsers_ApplicationUserID",
                        column: x => x.ApplicationUserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserPaymentSummaries_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserPaymentSummaries_PaymentID",
                table: "AppUserPaymentSummaries",
                column: "PaymentID");
        }
    }
}
