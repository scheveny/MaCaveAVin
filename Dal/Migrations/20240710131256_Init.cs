using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dal.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CellarCategory",
                columns: table => new
                {
                    CellarCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellarCategory", x => x.CellarCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CellarModel",
                columns: table => new
                {
                    CellarModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellarBrand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CellarTemperature = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CellarModel", x => x.CellarModelId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Cellar",
                columns: table => new
                {
                    CellarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CellarName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NbRow = table.Column<int>(type: "int", nullable: false),
                    NbStackRow = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CellarCategoryId = table.Column<int>(type: "int", nullable: true),
                    CellarModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cellar", x => x.CellarId);
                    table.ForeignKey(
                        name: "FK_Cellar_CellarCategory_CellarCategoryId",
                        column: x => x.CellarCategoryId,
                        principalTable: "CellarCategory",
                        principalColumn: "CellarCategoryId");
                    table.ForeignKey(
                        name: "FK_Cellar_CellarModel_CellarModelId",
                        column: x => x.CellarModelId,
                        principalTable: "CellarModel",
                        principalColumn: "CellarModelId");
                    table.ForeignKey(
                        name: "FK_Cellar_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bottle",
                columns: table => new
                {
                    BottleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BottleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BottleYear = table.Column<int>(type: "int", nullable: false),
                    WineColor = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Appellation = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PeakStart = table.Column<int>(type: "int", nullable: false),
                    PeakEnd = table.Column<int>(type: "int", nullable: false),
                    IdealPeak = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DrawerNb = table.Column<int>(type: "int", nullable: false),
                    StackInDrawerNb = table.Column<int>(type: "int", nullable: false),
                    CellarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bottle", x => x.BottleId);
                    table.ForeignKey(
                        name: "FK_Bottle_Cellar_CellarId",
                        column: x => x.CellarId,
                        principalTable: "Cellar",
                        principalColumn: "CellarId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bottle_CellarId",
                table: "Bottle",
                column: "CellarId");

            migrationBuilder.CreateIndex(
                name: "IX_Cellar_CellarCategoryId",
                table: "Cellar",
                column: "CellarCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Cellar_CellarModelId",
                table: "Cellar",
                column: "CellarModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Cellar_UserId",
                table: "Cellar",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bottle");

            migrationBuilder.DropTable(
                name: "Cellar");

            migrationBuilder.DropTable(
                name: "CellarCategory");

            migrationBuilder.DropTable(
                name: "CellarModel");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
