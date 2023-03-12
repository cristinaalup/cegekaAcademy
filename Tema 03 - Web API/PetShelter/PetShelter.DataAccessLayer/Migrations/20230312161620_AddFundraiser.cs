using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShelter.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddFundraiser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FundraiserId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FundraiserId",
                table: "Donations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Fundraisers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonationTarget = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDonations = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fundraiser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_FundraiserId",
                table: "Persons",
                column: "FundraiserId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_FundraiserId",
                table: "Donations",
                column: "FundraiserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Donations_Fundraiser_FundraiserId",
                table: "Donations",
                column: "FundraiserId",
                principalTable: "Fundraisers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Fundraiser_FundraiserId",
                table: "Persons",
                column: "FundraiserId",
                principalTable: "Fundraisers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
              name: "FK_Donations_Fundraiser_FundraiserId",
              table: "Donations");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Fundraiser_FundraiserId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "Fundraisers");

            migrationBuilder.DropIndex(
                name: "IX_Persons_FundraiserId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Donations_FundraiserId",
                table: "Donations");

            migrationBuilder.DropColumn(
                name: "FundraiserId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "FundraiserId",
                table: "Donations");
        }
    }
}
