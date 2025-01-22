using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class CompanyAddToRedemptionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "RedemptionModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RedemptionModels_CompanyModels_CompanyId",
                table: "RedemptionModels",
                column: "CompanyId",
                principalTable: "CompanyModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedemptionModels_CompanyModels_CompanyId",
                table: "RedemptionModels");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "RedemptionModels");
        
        }
    }
}
