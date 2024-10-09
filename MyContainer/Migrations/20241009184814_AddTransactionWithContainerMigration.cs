using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyContainer.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionWithContainerMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContainerId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ContainerId",
                table: "Transactions",
                column: "ContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Containers_ContainerId",
                table: "Transactions",
                column: "ContainerId",
                principalTable: "Containers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Containers_ContainerId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_ContainerId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "ContainerId",
                table: "Transactions");
        }
    }
}
