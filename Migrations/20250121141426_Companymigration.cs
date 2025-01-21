using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Companymigration : Migration
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
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyModels", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "CompanyModels",
                columns: new[] { "Id", "CompanyName", "Email", "Password", "RedemptionId" },
                values: new object[,]
                {
                    { 1, "Zelene tehnologije", "zelene.tehnologije@gmail.com", "Zelene12.", null },
                    { 2, "Plave tehnologije", "plave.tehnologije@gmail.com", "Plave12.", null },
                    { 3, "Cistoca", "cistoca@gmail.com", "Cistoca.1", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyModels_RedemptionId",
                table: "CompanyModels",
                column: "RedemptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyModels");
        }
    }
}
