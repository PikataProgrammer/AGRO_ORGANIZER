using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace AgroOrganizer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DriverName = table.Column<string>(type: "longtext", nullable: false),
                    DriverAge = table.Column<int>(type: "int", nullable: false),
                    DriverPhoneNumber = table.Column<string>(type: "longtext", nullable: false),
                    LicenseNumber = table.Column<string>(type: "longtext", nullable: true),
                    HiredOn = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    Email = table.Column<string>(type: "longtext", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: false),
                    PasswordSalt = table.Column<string>(type: "longtext", nullable: false),
                    ShouldChangePassword = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FieldName = table.Column<string>(type: "longtext", nullable: false),
                    FieldSize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FieldLocation = table.Column<string>(type: "longtext", nullable: false),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fields_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateSigned = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    FilePath = table.Column<string>(type: "longtext", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FieldSeasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CropType = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldSeasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldSeasons_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    Notes = table.Column<string>(type: "longtext", nullable: true),
                    FieldSeasonId = table.Column<int>(type: "int", nullable: false),
                    DriverId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Activities_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Activities_FieldSeasons_FieldSeasonId",
                        column: x => x.FieldSeasonId,
                        principalTable: "FieldSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(type: "longtext", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    FieldSeasonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_FieldSeasons_FieldSeasonId",
                        column: x => x.FieldSeasonId,
                        principalTable: "FieldSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DateSigned = table.Column<DateTimeOffset>(type: "datetime", nullable: false),
                    CropType = table.Column<int>(type: "int", nullable: false),
                    PriceForKg = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<double>(type: "double", nullable: false),
                    BuyerName = table.Column<string>(type: "longtext", nullable: false),
                    FieldSeasonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sales_FieldSeasons_FieldSeasonId",
                        column: x => x.FieldSeasonId,
                        principalTable: "FieldSeasons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DriverId",
                table: "Activities",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_FieldSeasonId",
                table: "Activities",
                column: "FieldSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_FieldId",
                table: "Contracts",
                column: "FieldId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_FieldSeasonId",
                table: "Expenses",
                column: "FieldSeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_UserId",
                table: "Fields",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldSeasons_FieldId",
                table: "FieldSeasons",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_FieldSeasonId",
                table: "Sales",
                column: "FieldSeasonId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Sales");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "FieldSeasons");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
