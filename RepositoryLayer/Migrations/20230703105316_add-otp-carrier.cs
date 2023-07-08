using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class addotpcarrier : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                table: "OTP_TYPE",
                type: "NVARCHAR2(2000)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EMAIL_ADDRESS",
                table: "OTP",
                type: "NVARCHAR2(2000)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OTP_CARRIER_ID",
                table: "OTP",
                type: "NUMBER(10)",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "OTP_CARRIER",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    OTP_CARRIER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IsActive = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP_CARRIER", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OTP_OTP_CARRIER_ID",
                table: "OTP",
                column: "OTP_CARRIER_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OTP_OTP_TYPE_OTP_CARRIER_ID",
                table: "OTP",
                column: "OTP_CARRIER_ID",
                principalTable: "OTP_TYPE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OTP_OTP_TYPE_OTP_CARRIER_ID",
                table: "OTP");

            migrationBuilder.DropTable(
                name: "OTP_CARRIER");

            migrationBuilder.DropIndex(
                name: "IX_OTP_OTP_CARRIER_ID",
                table: "OTP");

            migrationBuilder.DropColumn(
                name: "EMAIL_ADDRESS",
                table: "OTP");

            migrationBuilder.DropColumn(
                name: "OTP_CARRIER_ID",
                table: "OTP");

            migrationBuilder.AlterColumn<string>(
                name: "NAME",
                table: "OTP_TYPE",
                type: "NVARCHAR2(2000)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "NVARCHAR2(2000)");
        }
    }
}
