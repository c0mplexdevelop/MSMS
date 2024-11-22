using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MSMS.Migrations.LabMigrations
{
    /// <inheritdoc />
    public partial class PatientSeedDataMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActiveProcedures",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProcedureServiceDateTime",
                value: new DateTime(2024, 11, 22, 22, 26, 52, 7, DateTimeKind.Local).AddTicks(3622));

            migrationBuilder.UpdateData(
                table: "ActiveProcedures",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProcedureServiceDateTime",
                value: new DateTime(2024, 11, 21, 22, 26, 52, 7, DateTimeKind.Local).AddTicks(3623));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Age",
                value: 40);

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Age",
                value: 30);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ActiveProcedures",
                keyColumn: "Id",
                keyValue: 1,
                column: "ProcedureServiceDateTime",
                value: new DateTime(2024, 11, 22, 22, 11, 6, 232, DateTimeKind.Local).AddTicks(5666));

            migrationBuilder.UpdateData(
                table: "ActiveProcedures",
                keyColumn: "Id",
                keyValue: 2,
                column: "ProcedureServiceDateTime",
                value: new DateTime(2024, 11, 21, 22, 11, 6, 232, DateTimeKind.Local).AddTicks(5667));

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 1,
                column: "Age",
                value: null);

            migrationBuilder.UpdateData(
                table: "Patients",
                keyColumn: "Id",
                keyValue: 2,
                column: "Age",
                value: null);
        }
    }
}
