using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MSMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Procedure",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcedurePrice = table.Column<decimal>(type: "decimal(38,2)", precision: 38, scale: 2, nullable: false),
                    ProcedureDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProcedureNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedure", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActiveProcedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ProcedureId = table.Column<int>(type: "int", nullable: false),
                    ProcedureServiceDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveProcedures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveProcedures_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActiveProcedures_Procedure_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedure",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GenericName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Dosage = table.Column<int>(type: "int", nullable: false),
                    ExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    SuppliedDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(38,2)", precision: 38, scale: 2, nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ReorderLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medicines_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateOnly>(type: "date", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReferenceType = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "ContactNumber", "FirstName", "LastName", "MiddleName" },
                values: new object[,]
                {
                    { 1, "1234567890", "John", "Smith", "Doe" },
                    { 2, "0987654321", "Jane", "Smith", "Doe" }
                });

            migrationBuilder.InsertData(
                table: "Procedure",
                columns: new[] { "Id", "ProcedureDescription", "ProcedureName", "ProcedureNotes", "ProcedurePrice" },
                values: new object[,]
                {
                    { 1, "Blood Test Duh", "Blood Test", "Disposable Syringe", 100.00m },
                    { 2, "Analyze patient urine for bacteria or unbalanced electrolyte level.", "Urinalysis", "Make Patient drink water till they excrete.", 100.00m }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "ContactNumber", "Name" },
                values: new object[,]
                {
                    { 1, "Address 1", "1234567890", "Supplier 1" },
                    { 2, "Address 2", "0987654321", "Supplier 2" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "", "John Doe", "test123", 0, "c0mplex" },
                    { 2, "", "Jane Doe", "test123", 3, "test123" }
                });

            migrationBuilder.InsertData(
                table: "ActiveProcedures",
                columns: new[] { "Id", "PatientId", "ProcedureId", "ProcedureServiceDateTime" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2024, 11, 20, 14, 32, 44, 775, DateTimeKind.Local).AddTicks(3932) },
                    { 2, 2, 1, new DateTime(2024, 11, 19, 14, 32, 44, 775, DateTimeKind.Local).AddTicks(3934) }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "BrandName", "Dosage", "ExpiryDate", "GenericName", "Price", "Quantity", "ReorderLevel", "SuppliedDate", "SupplierId" },
                values: new object[,]
                {
                    { 1, "", 0, new DateOnly(1, 1, 1), "Paracetamol", 10.00m, 100, 50, new DateOnly(1, 1, 1), 1 },
                    { 2, "", 0, new DateOnly(1, 1, 1), "Aspirin", 5.00m, 50, 75, new DateOnly(1, 1, 1), 2 }
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "CreatedAt", "Message", "ReferenceType", "Title", "UserId" },
                values: new object[] { 1, new DateOnly(2024, 11, 20), "Hello, World!", 0, "", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveProcedures_PatientId",
                table: "ActiveProcedures",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveProcedures_ProcedureId",
                table: "ActiveProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_SupplierId",
                table: "Medicines",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveProcedures");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Procedure");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
