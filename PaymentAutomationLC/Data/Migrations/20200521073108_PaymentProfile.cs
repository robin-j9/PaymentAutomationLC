using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentAutomationLC.Data.Migrations
{
    public partial class PaymentProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentProfileID",
                table: "Payments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentProfiles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PayPerArticle = table.Column<double>(nullable: false),
                    ArticleBonus = table.Column<double>(nullable: false),
                    MinimumPVForBonus = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProfiles", x => x.ID);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PaymentProfiles_PaymentProfileID",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PaymentProfiles");

            migrationBuilder.DropIndex(
                name: "IX_Payments_PaymentProfileID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "PaymentProfileID",
                table: "Payments");
        }
    }
}
