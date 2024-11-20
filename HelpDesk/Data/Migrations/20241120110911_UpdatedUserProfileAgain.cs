using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserProfileAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_UserRoleProfiles_RoleId",
                table: "UserRoleProfiles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRoleProfiles_AspNetRoles_RoleId",
                table: "UserRoleProfiles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRoleProfiles_AspNetRoles_RoleId",
                table: "UserRoleProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleProfiles_RoleId",
                table: "UserRoleProfiles");
        }
    }
}
