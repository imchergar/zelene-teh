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
                name: "CompanyModelId",
                table: "RedemptionModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RedemptionModels_CompanyModels_CompanyModelId",
                table: "RedemptionModels",
                column: "CompanyModelId",
                principalTable: "CompanyModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RedemptionModels_CompanyModels_CompanyModelId",
                table: "RedemptionModels");

            migrationBuilder.DropColumn(
                name: "CompanyModelId",
                table: "RedemptionModels");
        
        }
    }
}
