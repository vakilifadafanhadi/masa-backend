using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace masa_backend.Migrations
{
    public partial class wallet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WalletHistories_Wallets_WalletId",
                table: "WalletHistories");

            migrationBuilder.DropIndex(
                name: "IX_WalletHistories_WalletId",
                table: "WalletHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Wallets",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_WalletId",
                table: "Wallets",
                column: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_WalletHistories_WalletId",
                table: "Wallets",
                column: "WalletId",
                principalTable: "WalletHistories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_WalletHistories_WalletId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_WalletId",
                table: "Wallets");

            migrationBuilder.DropColumn(
                name: "WalletId",
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
    }
}
