using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace evacuation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedMasterData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "prefix",
                table: "RunningCodes",
                newName: "Prefix");

            migrationBuilder.InsertData(
                table: "EvacuationStatuses",
                columns: new[] { "Id", "CreateDate", "Description", "Sequence", "StatusCode", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("423c1ccb-fcff-44f6-a9c5-4e5bcb84f5f6"), new DateTime(2026, 1, 22, 11, 52, 14, 0, DateTimeKind.Unspecified), "กำลังดำเนินการ", 2, "INPROGRESS", null },
                    { new Guid("4464588d-bd4f-4ad9-ba60-217b8b163e61"), new DateTime(2026, 1, 22, 11, 52, 14, 0, DateTimeKind.Unspecified), "พร้อมดำเนินการ", 1, "READY", null },
                    { new Guid("56c51499-c1d3-411a-b7e4-37d43a6e0bda"), new DateTime(2026, 1, 22, 11, 52, 14, 0, DateTimeKind.Unspecified), "เสร็จสิ้น", 3, "COMPLETED", null }
                });

            migrationBuilder.InsertData(
                table: "RunningCodes",
                columns: new[] { "Name", "CurrentValue", "Prefix" },
                values: new object[,]
                {
                    { "plan", 0, "P" },
                    { "vehicle", 0, "V" },
                    { "zone", 0, "Z" }
                });

            migrationBuilder.InsertData(
                table: "VehicleTypes",
                columns: new[] { "Id", "CreateDate", "TypeName", "UpdateDate" },
                values: new object[,]
                {
                    { new Guid("8e637482-a0c2-45a2-b97e-0c8c87e1b120"), new DateTime(2026, 1, 22, 15, 1, 53, 0, DateTimeKind.Unspecified), "bus", null },
                    { new Guid("bbbd36d6-3b89-4fde-927c-0cddf4d1f9f1"), new DateTime(2026, 1, 22, 15, 2, 1, 0, DateTimeKind.Unspecified), "boat", null },
                    { new Guid("ddf12d9d-c3e0-460b-9730-145e2016c27d"), new DateTime(2026, 1, 22, 15, 1, 57, 0, DateTimeKind.Unspecified), "van", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvacuationPlans_VehicleId",
                table: "EvacuationPlans",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_EvacuationPlans_ZoneId",
                table: "EvacuationPlans",
                column: "ZoneId");

            migrationBuilder.AddForeignKey(
                name: "FK_EvacuationPlans_EvacuationZones_ZoneId",
                table: "EvacuationPlans",
                column: "ZoneId",
                principalTable: "EvacuationZones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EvacuationPlans_Vehicles_VehicleId",
                table: "EvacuationPlans",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvacuationPlans_EvacuationZones_ZoneId",
                table: "EvacuationPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_EvacuationPlans_Vehicles_VehicleId",
                table: "EvacuationPlans");

            migrationBuilder.DropIndex(
                name: "IX_EvacuationPlans_VehicleId",
                table: "EvacuationPlans");

            migrationBuilder.DropIndex(
                name: "IX_EvacuationPlans_ZoneId",
                table: "EvacuationPlans");

            migrationBuilder.DeleteData(
                table: "EvacuationStatuses",
                keyColumn: "Id",
                keyValue: new Guid("423c1ccb-fcff-44f6-a9c5-4e5bcb84f5f6"));

            migrationBuilder.DeleteData(
                table: "EvacuationStatuses",
                keyColumn: "Id",
                keyValue: new Guid("4464588d-bd4f-4ad9-ba60-217b8b163e61"));

            migrationBuilder.DeleteData(
                table: "EvacuationStatuses",
                keyColumn: "Id",
                keyValue: new Guid("56c51499-c1d3-411a-b7e4-37d43a6e0bda"));

            migrationBuilder.DeleteData(
                table: "RunningCodes",
                keyColumn: "Name",
                keyValue: "plan");

            migrationBuilder.DeleteData(
                table: "RunningCodes",
                keyColumn: "Name",
                keyValue: "vehicle");

            migrationBuilder.DeleteData(
                table: "RunningCodes",
                keyColumn: "Name",
                keyValue: "zone");

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("8e637482-a0c2-45a2-b97e-0c8c87e1b120"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("bbbd36d6-3b89-4fde-927c-0cddf4d1f9f1"));

            migrationBuilder.DeleteData(
                table: "VehicleTypes",
                keyColumn: "Id",
                keyValue: new Guid("ddf12d9d-c3e0-460b-9730-145e2016c27d"));

            migrationBuilder.RenameColumn(
                name: "Prefix",
                table: "RunningCodes",
                newName: "prefix");
        }
    }
}
