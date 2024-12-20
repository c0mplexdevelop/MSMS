﻿using System;
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
                name: "Accounts",
                columns: table => new
                {
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Schedule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubSystem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ErrorMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.EmployeeNumber);
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
                    AccountId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Height = table.Column<int>(type: "int", nullable: true),
                    BloodPressure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Employer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpouseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyPerson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyRelationship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmergencyContact = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PastMedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FamilyHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InsuranceDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimaryCareProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentGuardianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentGuardianContact = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    RecordDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentMedications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActiveProcedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ProcedureId = table.Column<int>(type: "int", nullable: false),
                    ProcedureServiceDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPaid = table.Column<bool>(type: "bit", nullable: false)
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
                name: "Diagnoses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    MedicalRecordId = table.Column<int>(type: "int", nullable: false),
                    DiagnosisDetails = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MedicalRecordId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diagnoses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Diagnoses_MedicalRecords_MedicalRecordId",
                        column: x => x.MedicalRecordId,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Diagnoses_MedicalRecords_MedicalRecordId1",
                        column: x => x.MedicalRecordId1,
                        principalTable: "MedicalRecords",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Diagnoses_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Notifications",
                columns: new[] { "Id", "AccountId", "CreatedAt", "Message", "ReferenceType", "Title" },
                values: new object[] { 1, 1, new DateOnly(2024, 12, 4), "Hello, World!", 0, "" });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Age", "BloodPressure", "ContactNumber", "DateOfBirth", "EmergencyContact", "EmergencyPerson", "EmergencyRelationship", "Employer", "FamilyHistory", "FirstName", "Gender", "Height", "InsuranceDetails", "LastName", "MiddleName", "Occupation", "ParentGuardianContact", "ParentGuardianName", "PastMedicalHistory", "PrimaryCareProvider", "SpouseName", "Weight" },
                values: new object[,]
                {
                    { 1, 40, null, "1234567890", new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, "John", 0, null, null, "Smith", "Doe", null, null, null, null, null, null, null },
                    { 2, 30, null, "0987654321", new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, null, null, null, "Jane", 1, null, null, "Smith", "Doe", null, null, null, null, null, null, null }
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
                table: "ActiveProcedures",
                columns: new[] { "Id", "IsPaid", "PatientId", "ProcedureId", "ProcedureServiceDateTime" },
                values: new object[,]
                {
                    { 1, false, 1, 1, new DateTime(2024, 12, 4, 0, 41, 56, 825, DateTimeKind.Local).AddTicks(3085) },
                    { 2, false, 2, 1, new DateTime(2024, 12, 3, 0, 41, 56, 825, DateTimeKind.Local).AddTicks(3086) }
                });

            migrationBuilder.InsertData(
                table: "MedicalRecords",
                columns: new[] { "Id", "CreatedAt", "CurrentMedications", "Doctor", "MedicalHistory", "Notes", "PatientId", "RecordDetails", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 3, 16, 41, 56, 825, DateTimeKind.Utc).AddTicks(2995), "Paracetamol", "Dr. John Doe", null, null, 1, null, new DateTime(2024, 12, 3, 16, 41, 56, 825, DateTimeKind.Utc).AddTicks(2996) },
                    { 2, new DateTime(2024, 12, 3, 16, 41, 56, 825, DateTimeKind.Utc).AddTicks(2997), "Paracetamol", "Dra. Jane Doe", null, null, 2, null, new DateTime(2024, 12, 3, 16, 41, 56, 825, DateTimeKind.Utc).AddTicks(2998) }
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
                table: "Diagnoses",
                columns: new[] { "Id", "CreatedAt", "DiagnosisDetails", "Doctor", "MedicalRecordId", "MedicalRecordId1", "Notes", "PatientId" },
                values: new object[,]
                {
                    { 1, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chickenpox Varicella", "Dr. John Doe", 1, null, "high Fever and Fatigue", 1 },
                    { 2, new DateTime(2021, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Influenza", "Dra. Jane Doe", 2, null, "Mucus on tne alveoli.", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveProcedures_PatientId",
                table: "ActiveProcedures",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ActiveProcedures_ProcedureId",
                table: "ActiveProcedures",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_MedicalRecordId",
                table: "Diagnoses",
                column: "MedicalRecordId");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_MedicalRecordId1",
                table: "Diagnoses",
                column: "MedicalRecordId1");

            migrationBuilder.CreateIndex(
                name: "IX_Diagnoses_PatientId",
                table: "Diagnoses",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_SupplierId",
                table: "Medicines",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "ActiveProcedures");

            migrationBuilder.DropTable(
                name: "Diagnoses");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Procedure");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Patients");
        }
    }
}
