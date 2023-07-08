using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class addotpclassascontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OTP_TYPE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    CHAR_LENGTH = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    VALID_TILL = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IsActive = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OTP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    MOBILE_NUMBER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    GENERATION_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    OPT_TYPE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    IsActive = table.Column<bool>(type: "NUMBER(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OTP_OTP_TYPE_OPT_TYPE_ID",
                        column: x => x.OPT_TYPE_ID,
                        principalTable: "OTP_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OTP_OPT_TYPE_ID",
                table: "OTP",
                column: "OPT_TYPE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OTP");

            migrationBuilder.DropTable(
                name: "OTP_TYPE");
        }
    }
}
