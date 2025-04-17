using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetAPI.Migrations
{
    /// <inheritdoc />
    public partial class SoftwareDBchanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "licenseKey",
                table: "Assets",
                newName: "LicenseKey");

            migrationBuilder.RenameColumn(
                name: "expiryDate",
                table: "Assets",
                newName: "ExpiryDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LicenseKey",
                table: "Assets",
                newName: "licenseKey");

            migrationBuilder.RenameColumn(
                name: "ExpiryDate",
                table: "Assets",
                newName: "expiryDate");
        }
    }
}
