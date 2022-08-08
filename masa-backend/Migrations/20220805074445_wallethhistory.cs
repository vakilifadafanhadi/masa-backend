using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace masa_backend.Migrations
{
    public partial class wallethhistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletHistories_WalletId",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "WalletId",
                table: "Wallets",
                newName: "WalletHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_WalletId",
                table: "Wallets",
                newName: "IX_Wallets_WalletHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletHistories_WalletHistoryId",
                table: "Wallets",
                column: "WalletHistoryId",
                principalTable: "WalletHistories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletHistories_WalletHistoryId",
                table: "Wallets");

            migrationBuilder.RenameColumn(
                name: "WalletHistoryId",
                table: "Wallets",
                newName: "WalletId");

            migrationBuilder.RenameIndex(
                name: "IX_Wallets_WalletHistoryId",
                table: "Wallets",
                newName: "IX_Wallets_WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletHistories_WalletId",
                table: "Wallets",
                column: "WalletId",
                principalTable: "WalletHistories",
                principalColumn: "Id");
        }
    }
}
