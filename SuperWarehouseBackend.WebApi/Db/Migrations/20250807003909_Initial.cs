using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperWarehouseBackend.WebApi.Db.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "InboundDocuments",
                schema: "public",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboundDocuments", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "MeasureUnits",
                schema: "public",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasureUnits", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "Resources",
                schema: "public",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resources", x => x.Guid);
                });

            migrationBuilder.CreateTable(
                name: "ResourceTotalQuantities",
                schema: "public",
                columns: table => new
                {
                    ResourceGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    MeasureUnitGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(20,10)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourceTotalQuantities", x => new { x.ResourceGuid, x.MeasureUnitGuid });
                });

            migrationBuilder.CreateTable(
                name: "InboundResources",
                schema: "public",
                columns: table => new
                {
                    InboundDocumentGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    ResourceGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    MeasureUnitGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric(20,10)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "bytea", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InboundResources", x => new { x.InboundDocumentGuid, x.ResourceGuid, x.MeasureUnitGuid });
                    table.ForeignKey(
                        name: "FK_InboundResources_InboundDocuments_InboundDocumentGuid",
                        column: x => x.InboundDocumentGuid,
                        principalSchema: "public",
                        principalTable: "InboundDocuments",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InboundDocuments_Number",
                schema: "public",
                table: "InboundDocuments",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeasureUnits_Name",
                schema: "public",
                table: "MeasureUnits",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resources_Name",
                schema: "public",
                table: "Resources",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InboundResources",
                schema: "public");

            migrationBuilder.DropTable(
                name: "MeasureUnits",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Resources",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ResourceTotalQuantities",
                schema: "public");

            migrationBuilder.DropTable(
                name: "InboundDocuments",
                schema: "public");
        }
    }
}
