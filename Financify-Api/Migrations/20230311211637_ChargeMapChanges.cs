using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financify_Api.Migrations
{
    /// <inheritdoc />
    public partial class ChargeMapChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charge_Accounts_AccountId",
                table: "Charge");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charge",
                table: "Charge");

            migrationBuilder.RenameTable(
                name: "Charge",
                newName: "Charges");

            migrationBuilder.RenameIndex(
                name: "IX_Charge_AccountId",
                table: "Charges",
                newName: "IX_Charges_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Charges",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Charges",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charges",
                table: "Charges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Charges_Accounts_AccountId",
                table: "Charges",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Charges_Accounts_AccountId",
                table: "Charges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Charges",
                table: "Charges");

            migrationBuilder.RenameTable(
                name: "Charges",
                newName: "Charge");

            migrationBuilder.RenameIndex(
                name: "IX_Charges_AccountId",
                table: "Charge",
                newName: "IX_Charge_AccountId");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Charge",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Charge",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Charge",
                table: "Charge",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Charge_Accounts_AccountId",
                table: "Charge",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
