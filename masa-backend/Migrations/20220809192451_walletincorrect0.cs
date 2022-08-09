using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace masa_backend.Migrations
{
    public partial class walletincorrect0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletHistories_WalletHistoryId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_WalletHistoryId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "WalletHistoryId",
                table: "Wallets");

            migrationBuilder.CreateIndex(
                name: "IX_WalletHistories_WalletId",
                table: "WalletHistories",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_WalletHistories_Wallets_WalletId",
                table: "WalletHistories",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletHistories_Wallets_WalletId",
                table: "WalletHistories");

            migrationBuilder.DropIndex(
                name: "IX_WalletHistories_WalletId",
                table: "WalletHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "WalletHistoryId",
                table: "Wallets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_WalletHistoryId",
                table: "Wallets",
                column: "WalletHistoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletHistories_WalletHistoryId",
                table: "Wallets",
                column: "WalletHistoryId",
                principalTable: "WalletHistories",
                principalColumn: "Id");
        }
    }
}
