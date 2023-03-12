using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShelter.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fundraisers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DonationTarget = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fundraisers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonorId = table.Column<int>(type: "int", nullable: false),
                    FundraiserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donations_Fundraisers_FundraiserId",
                        column: x => x.FundraiserId,
                        principalTable: "Fundraisers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Donations_Persons_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FundraiserPerson",
                columns: table => new
                {
                    DonorsId = table.Column<int>(type: "int", nullable: false),
                    FundraisersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FundraiserPerson", x => new { x.DonorsId, x.FundraisersId });
                    table.ForeignKey(
                        name: "FK_FundraiserPerson_Fundraisers_FundraisersId",
                        column: x => x.FundraisersId,
                        principalTable: "Fundraisers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FundraiserPerson_Persons_DonorsId",
                        column: x => x.DonorsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 5000, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsHealthy = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    WeightInKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsSheltered = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RescuerId = table.Column<int>(type: "int", nullable: true),
                    AdopterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Persons_AdopterId",
                        column: x => x.AdopterId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pets_Persons_RescuerId",
                        column: x => x.RescuerId,
                        principalTable: "Persons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId",
                table: "Donations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_FundraiserId",
                table: "Donations",
                column: "FundraiserId");

            migrationBuilder.CreateIndex(
                name: "IX_FundraiserPerson_FundraisersId",
                table: "FundraiserPerson",
                column: "FundraisersId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_AdopterId",
                table: "Pets",
                column: "AdopterId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_RescuerId",
                table: "Pets",
                column: "RescuerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");

            migrationBuilder.DropTable(
                name: "FundraiserPerson");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Fundraisers");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
