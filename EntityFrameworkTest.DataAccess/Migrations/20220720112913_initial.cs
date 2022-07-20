using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EntityFrameworkTest.DataAccess.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    AdeptReference = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.AccountNumber);
                    table.UniqueConstraint("AK_Users_AdeptReference", x => x.AdeptReference);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    Line1 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Owner = table.Column<string>(type: "nvarchar(20)", nullable: true),
                    Line2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => new { x.Line1, x.City, x.Postcode });
                    table.ForeignKey(
                        name: "FK_Address_Users_Owner",
                        column: x => x.Owner,
                        principalTable: "Users",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Email",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    owner = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Email", x => x.EmailAddress);
                    table.ForeignKey(
                        name: "FK_Email_Users_owner",
                        column: x => x.owner,
                        principalTable: "Users",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdeptReference = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentSource = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payments_Users_AdeptReference",
                        column: x => x.AdeptReference,
                        principalTable: "Users",
                        principalColumn: "AdeptReference",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Phone",
                columns: table => new
                {
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    owner = table.Column<string>(type: "nvarchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phone", x => x.PhoneNumber);
                    table.ForeignKey(
                        name: "FK_Phone_Users_owner",
                        column: x => x.owner,
                        principalTable: "Users",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Address_Owner",
                table: "Address",
                column: "Owner");

            migrationBuilder.CreateIndex(
                name: "IX_Email_owner",
                table: "Email",
                column: "owner");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_AdeptReference",
                table: "Payments",
                column: "AdeptReference");

            migrationBuilder.CreateIndex(
                name: "IX_Phone_owner",
                table: "Phone",
                column: "owner");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "Email");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Phone");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
