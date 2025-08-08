using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperWarehouseBackend.WebApi.Db.Migrations
{
    /// <inheritdoc />
    public partial class RowVersion_changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "public",
                table: "ResourceTotalQuantities");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "public",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "public",
                table: "MeasureUnits");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "public",
                table: "InboundResources");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "public",
                table: "InboundDocuments");

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "public",
                table: "ResourceTotalQuantities",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "public",
                table: "Resources",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "public",
                table: "MeasureUnits",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "public",
                table: "InboundResources",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.AddColumn<uint>(
                name: "xmin",
                schema: "public",
                table: "InboundDocuments",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u);

            migrationBuilder.CreateIndex(
                name: "IX_ResourceTotalQuantities_MeasureUnitGuid",
                schema: "public",
                table: "ResourceTotalQuantities",
                column: "MeasureUnitGuid");

            migrationBuilder.CreateIndex(
                name: "IX_InboundResources_MeasureUnitGuid",
                schema: "public",
                table: "InboundResources",
                column: "MeasureUnitGuid");

            migrationBuilder.CreateIndex(
                name: "IX_InboundResources_ResourceGuid",
                schema: "public",
                table: "InboundResources",
                column: "ResourceGuid");

            migrationBuilder.AddForeignKey(
                name: "FK_InboundResources_MeasureUnits_MeasureUnitGuid",
                schema: "public",
                table: "InboundResources",
                column: "MeasureUnitGuid",
                principalSchema: "public",
                principalTable: "MeasureUnits",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InboundResources_Resources_ResourceGuid",
                schema: "public",
                table: "InboundResources",
                column: "ResourceGuid",
                principalSchema: "public",
                principalTable: "Resources",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceTotalQuantities_MeasureUnits_MeasureUnitGuid",
                schema: "public",
                table: "ResourceTotalQuantities",
                column: "MeasureUnitGuid",
                principalSchema: "public",
                principalTable: "MeasureUnits",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ResourceTotalQuantities_Resources_ResourceGuid",
                schema: "public",
                table: "ResourceTotalQuantities",
                column: "ResourceGuid",
                principalSchema: "public",
                principalTable: "Resources",
                principalColumn: "Guid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InboundResources_MeasureUnits_MeasureUnitGuid",
                schema: "public",
                table: "InboundResources");

            migrationBuilder.DropForeignKey(
                name: "FK_InboundResources_Resources_ResourceGuid",
                schema: "public",
                table: "InboundResources");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceTotalQuantities_MeasureUnits_MeasureUnitGuid",
                schema: "public",
                table: "ResourceTotalQuantities");

            migrationBuilder.DropForeignKey(
                name: "FK_ResourceTotalQuantities_Resources_ResourceGuid",
                schema: "public",
                table: "ResourceTotalQuantities");

            migrationBuilder.DropIndex(
                name: "IX_ResourceTotalQuantities_MeasureUnitGuid",
                schema: "public",
                table: "ResourceTotalQuantities");

            migrationBuilder.DropIndex(
                name: "IX_InboundResources_MeasureUnitGuid",
                schema: "public",
                table: "InboundResources");

            migrationBuilder.DropIndex(
                name: "IX_InboundResources_ResourceGuid",
                schema: "public",
                table: "InboundResources");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "public",
                table: "ResourceTotalQuantities");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "public",
                table: "Resources");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "public",
                table: "MeasureUnits");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "public",
                table: "InboundResources");

            migrationBuilder.DropColumn(
                name: "xmin",
                schema: "public",
                table: "InboundDocuments");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "public",
                table: "ResourceTotalQuantities",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "public",
                table: "Resources",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "public",
                table: "MeasureUnits",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "public",
                table: "InboundResources",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "public",
                table: "InboundDocuments",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
