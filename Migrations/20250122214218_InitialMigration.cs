using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternalNumberSequences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrentValue = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalNumberSequences", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SellerModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Oib = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RedemptionModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalNumber = table.Column<int>(type: "int", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Note = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SellerIdentifier = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedemptionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RedemptionModels_CompanyModels_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "CompanyModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RedemptionModels_SellerModels_SellerId",
                        column: x => x.SellerId,
                        principalTable: "SellerModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitOfQuantity = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PricePerPeace = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RedemptionModelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemModels_RedemptionModels_RedemptionModelId",
                        column: x => x.RedemptionModelId,
                        principalTable: "RedemptionModels",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "CompanyModels",
                columns: new[] { "Id", "CompanyName", "Email", "Password" },
                values: new object[,]
                {
                    { 1, "Zelene tehnologije", "zelene.tehnologije@gmail.com", "Zelene12." },
                    { 2, "Plave tehnologije", "plave.tehnologije@gmail.com", "Plave12." },
                    { 3, "Cistoca", "cistoca@gmail.com", "Cistoca.1" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemModels_RedemptionModelId",
                table: "ItemModels",
                column: "RedemptionModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RedemptionModels_CompanyId",
                table: "RedemptionModels",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_RedemptionModels_SellerId",
                table: "RedemptionModels",
                column: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InternalNumberSequences");

            migrationBuilder.DropTable(
                name: "ItemModels");

            migrationBuilder.DropTable(
                name: "RedemptionModels");

            migrationBuilder.DropTable(
                name: "CompanyModels");

            migrationBuilder.DropTable(
                name: "SellerModels");
        }
    }
}
