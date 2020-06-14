using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentAutomationLC.Data.Migrations
{
    public partial class CalculationComplete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPayments_AspNetUsers_ApplicationUserID",
                table: "ApplicationUserPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPayments_Payments_PaymentID",
                table: "ApplicationUserPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPayments_PaymentProfiles_PaymentProfileID",
                table: "ApplicationUserPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Payments_PaymentID",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PaymentProfiles_PaymentProfileID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Payments",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "PaymentProfiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PaymentProfileID",
                table: "AspNetUsers",
                newName: "PaymentProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PaymentProfileID",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PaymentProfileId");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "Articles",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Articles",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_PaymentID",
                table: "Articles",
                newName: "IX_Articles_PaymentId");

            migrationBuilder.RenameColumn(
                name: "PaymentProfileID",
                table: "ApplicationUserPayments",
                newName: "PaymentProfileId");

            migrationBuilder.RenameColumn(
                name: "PaymentID",
                table: "ApplicationUserPayments",
                newName: "PaymentId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "ApplicationUserPayments",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserPayments_PaymentProfileID",
                table: "ApplicationUserPayments",
                newName: "IX_ApplicationUserPayments_PaymentProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserPayments_PaymentID",
                table: "ApplicationUserPayments",
                newName: "IX_ApplicationUserPayments_PaymentId");

            migrationBuilder.AddColumn<bool>(
                name: "CalculationComplete",
                table: "Payments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPayments_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserPayments",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPayments_Payments_PaymentId",
                table: "ApplicationUserPayments",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPayments_PaymentProfiles_PaymentProfileId",
                table: "ApplicationUserPayments",
                column: "PaymentProfileId",
                principalTable: "PaymentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Payments_PaymentId",
                table: "Articles",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PaymentProfiles_PaymentProfileId",
                table: "AspNetUsers",
                column: "PaymentProfileId",
                principalTable: "PaymentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPayments_AspNetUsers_ApplicationUserId",
                table: "ApplicationUserPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPayments_Payments_PaymentId",
                table: "ApplicationUserPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUserPayments_PaymentProfiles_PaymentProfileId",
                table: "ApplicationUserPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_Articles_Payments_PaymentId",
                table: "Articles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PaymentProfiles_PaymentProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CalculationComplete",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Payments",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentProfiles",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "PaymentProfileId",
                table: "AspNetUsers",
                newName: "PaymentProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_PaymentProfileId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_PaymentProfileID");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "Articles",
                newName: "PaymentID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Articles",
                newName: "ID");

            migrationBuilder.RenameIndex(
                name: "IX_Articles_PaymentId",
                table: "Articles",
                newName: "IX_Articles_PaymentID");

            migrationBuilder.RenameColumn(
                name: "PaymentProfileId",
                table: "ApplicationUserPayments",
                newName: "PaymentProfileID");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "ApplicationUserPayments",
                newName: "PaymentID");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ApplicationUserPayments",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserPayments_PaymentProfileId",
                table: "ApplicationUserPayments",
                newName: "IX_ApplicationUserPayments_PaymentProfileID");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationUserPayments_PaymentId",
                table: "ApplicationUserPayments",
                newName: "IX_ApplicationUserPayments_PaymentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPayments_AspNetUsers_ApplicationUserID",
                table: "ApplicationUserPayments",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPayments_Payments_PaymentID",
                table: "ApplicationUserPayments",
                column: "PaymentID",
                principalTable: "Payments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUserPayments_PaymentProfiles_PaymentProfileID",
                table: "ApplicationUserPayments",
                column: "PaymentProfileID",
                principalTable: "PaymentProfiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_Payments_PaymentID",
                table: "Articles",
                column: "PaymentID",
                principalTable: "Payments",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PaymentProfiles_PaymentProfileID",
                table: "AspNetUsers",
                column: "PaymentProfileID",
                principalTable: "PaymentProfiles",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
