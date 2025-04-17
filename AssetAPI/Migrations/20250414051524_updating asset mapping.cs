using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatingassetmapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetMappings_Assets_BookId",
                table: "AssetMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetMappings_Assets_HardwareId",
                table: "AssetMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetMappings_Assets_SoftwareLicenseId",
                table: "AssetMappings");

            migrationBuilder.DropIndex(
                name: "IX_AssetMappings_BookId",
                table: "AssetMappings");

            migrationBuilder.DropIndex(
                name: "IX_AssetMappings_HardwareId",
                table: "AssetMappings");

            migrationBuilder.DropIndex(
                name: "IX_AssetMappings_SoftwareLicenseId",
                table: "AssetMappings");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "AssetMappings");

            migrationBuilder.DropColumn(
                name: "HardwareId",
                table: "AssetMappings");

            migrationBuilder.DropColumn(
                name: "SoftwareLicenseId",
                table: "AssetMappings");

            migrationBuilder.AddColumn<int>(
                name: "AssetId",
                table: "AssetMappings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AssetMappings_AssetId",
                table: "AssetMappings",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMappings_Assets_AssetId",
                table: "AssetMappings",
                column: "AssetId",
                principalTable: "Assets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetMappings_Assets_AssetId",
                table: "AssetMappings");

            migrationBuilder.DropIndex(
                name: "IX_AssetMappings_AssetId",
                table: "AssetMappings");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "AssetMappings");

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "AssetMappings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HardwareId",
                table: "AssetMappings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoftwareLicenseId",
                table: "AssetMappings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetMappings_BookId",
                table: "AssetMappings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetMappings_HardwareId",
                table: "AssetMappings",
                column: "HardwareId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetMappings_SoftwareLicenseId",
                table: "AssetMappings",
                column: "SoftwareLicenseId");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMappings_Assets_BookId",
                table: "AssetMappings",
                column: "BookId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMappings_Assets_HardwareId",
                table: "AssetMappings",
                column: "HardwareId",
                principalTable: "Assets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMappings_Assets_SoftwareLicenseId",
                table: "AssetMappings",
                column: "SoftwareLicenseId",
                principalTable: "Assets",
                principalColumn: "Id");
        }
    }
}
