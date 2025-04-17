using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssetAPI.Migrations
{
    /// <inheritdoc />
    public partial class aadingasset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssetMappings_Books_BookId",
                table: "AssetMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetMappings_Hardwares_HardwareId",
                table: "AssetMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_AssetMappings_Softwares_SoftwareLicenseId",
                table: "AssetMappings");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Hardwares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Softwares",
                table: "Softwares");

            migrationBuilder.RenameTable(
                name: "Softwares",
                newName: "Assets");

            migrationBuilder.AlterColumn<string>(
                name: "licenseKey",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "expiryDate",
                table: "Assets",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Assets",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Assets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Assets",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Assets",
                table: "Assets",
                column: "Id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropPrimaryKey(
                name: "PK_Assets",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Assets");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Assets");

            migrationBuilder.RenameTable(
                name: "Assets",
                newName: "Softwares");

            migrationBuilder.AlterColumn<string>(
                name: "licenseKey",
                table: "Softwares",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "expiryDate",
                table: "Softwares",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Softwares",
                table: "Softwares",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PublishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hardwares",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuantityAvailable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hardwares", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMappings_Books_BookId",
                table: "AssetMappings",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMappings_Hardwares_HardwareId",
                table: "AssetMappings",
                column: "HardwareId",
                principalTable: "Hardwares",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AssetMappings_Softwares_SoftwareLicenseId",
                table: "AssetMappings",
                column: "SoftwareLicenseId",
                principalTable: "Softwares",
                principalColumn: "Id");
        }
    }
}
