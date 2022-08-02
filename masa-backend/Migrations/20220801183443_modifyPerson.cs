using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace masa_backend.Migrations
{
    public partial class modifyPerson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_Users_Id",
                table: "PersonalInformations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Users_PersonId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonId",
                table: "Users",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_PersonalInformations_PersonId",
                table: "Users",
                column: "PersonId",
                principalTable: "PersonalInformations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_PersonalInformations_PersonId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PersonId",
                table: "Users");

            migrationBuilder.AlterColumn<Guid>(
                name: "PersonId",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Users_PersonId",
                table: "Users",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_Users_Id",
                table: "PersonalInformations",
                column: "Id",
                principalTable: "Users",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
