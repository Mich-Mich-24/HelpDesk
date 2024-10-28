using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class SystemCodesActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodesDetails_SystemCodes_SystemCodeId",
                table: "SystemCodesDetails");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SystemCodesDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SystemCodesDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "SystemCodesDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "SystemCodesDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "SystemCodes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "SystemCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "SystemCodes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "SystemCodes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodesDetails_CreatedById",
                table: "SystemCodesDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodesDetails_ModifiedById",
                table: "SystemCodesDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodes_CreatedById",
                table: "SystemCodes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SystemCodes_ModifiedById",
                table: "SystemCodes",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodes_AspNetUsers_CreatedById",
                table: "SystemCodes",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodes_AspNetUsers_ModifiedById",
                table: "SystemCodes",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodesDetails_AspNetUsers_CreatedById",
                table: "SystemCodesDetails",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodesDetails_AspNetUsers_ModifiedById",
                table: "SystemCodesDetails",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodesDetails_SystemCodes_SystemCodeId",
                table: "SystemCodesDetails",
                column: "SystemCodeId",
                principalTable: "SystemCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodes_AspNetUsers_CreatedById",
                table: "SystemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodes_AspNetUsers_ModifiedById",
                table: "SystemCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodesDetails_AspNetUsers_CreatedById",
                table: "SystemCodesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodesDetails_AspNetUsers_ModifiedById",
                table: "SystemCodesDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_SystemCodesDetails_SystemCodes_SystemCodeId",
                table: "SystemCodesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodesDetails_CreatedById",
                table: "SystemCodesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodesDetails_ModifiedById",
                table: "SystemCodesDetails");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodes_CreatedById",
                table: "SystemCodes");

            migrationBuilder.DropIndex(
                name: "IX_SystemCodes_ModifiedById",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SystemCodesDetails");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SystemCodesDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "SystemCodesDetails");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "SystemCodesDetails");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "SystemCodes");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "SystemCodes");

            migrationBuilder.AddForeignKey(
                name: "FK_SystemCodesDetails_SystemCodes_SystemCodeId",
                table: "SystemCodesDetails",
                column: "SystemCodeId",
                principalTable: "SystemCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
