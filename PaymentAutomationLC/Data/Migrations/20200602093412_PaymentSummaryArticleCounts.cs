using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentAutomationLC.Data.Migrations
{
    public partial class PaymentSummaryArticleCounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumArticlesWithBonus",
                table: "ApplicationUserPayments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumArticlesWithoutBonus",
                table: "ApplicationUserPayments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumArticlesWithBonus",
                table: "ApplicationUserPayments");

            migrationBuilder.DropColumn(
                name: "NumArticlesWithoutBonus",
                table: "ApplicationUserPayments");
        }
    }
}
