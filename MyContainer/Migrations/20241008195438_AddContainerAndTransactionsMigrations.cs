using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyContainer.Migrations
{
    /// <inheritdoc />
    public partial class AddContainerAndTransactionsMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Containers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContainerType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContainerPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoadingPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoadingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfWoodenBoards = table.Column<int>(type: "int", nullable: false),
                    PriceOfWoodenBoard = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfBoxes = table.Column<int>(type: "int", nullable: false),
                    BoxPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NumberOfStretch = table.Column<int>(type: "int", nullable: false),
                    PriceOfStretch = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remaining = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Arrive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Containers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Containers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Containers_UserId",
                table: "Containers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Containers");

            migrationBuilder.DropTable(
                name: "Transaction");
        }
    }
}
