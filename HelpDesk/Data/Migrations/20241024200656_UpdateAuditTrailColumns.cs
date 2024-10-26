using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    public partial class UpdateAuditTrailColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Remove the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditTrails",
                table: "AuditTrails");

            // Drop the old 'Id' column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuditTrails");

            // Recreate 'Id' column as an identity column
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AuditTrails",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // Re-add the primary key constraint on 'Id'
            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditTrails",
                table: "AuditTrails",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remove the primary key constraint
            migrationBuilder.DropPrimaryKey(
                name: "PK_AuditTrails",
                table: "AuditTrails");

            // Drop the new 'Id' column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuditTrails");

            // Recreate the old 'Id' column as a string
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AuditTrails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            // Re-add the primary key constraint
            migrationBuilder.AddPrimaryKey(
                name: "PK_AuditTrails",
                table: "AuditTrails",
                column: "Id");
        }
    }
}
