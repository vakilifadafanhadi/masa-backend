using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace masa_backend.Migrations
{
    public partial class walletHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "WalletHistories",
                newName: "DtoRequest");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DtoRequest",
                table: "WalletHistories",
                newName: "TransactionId");
        }
    }
}
