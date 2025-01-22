using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddRedemptionItemRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RedemptionId",
                table: "ItemModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ItemModels_RedemptionId",
                table: "ItemModels",
                column: "RedemptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemModels_RedemptionModels_RedemptionId",
                table: "ItemModels",
                column: "RedemptionId",
                principalTable: "RedemptionModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemModels_RedemptionModels_RedemptionId",
                table: "ItemModels");

            migrationBuilder.DropIndex(
                name: "IX_ItemModels_RedemptionId",
                table: "ItemModels");

            migrationBuilder.DropColumn(
                name: "RedemptionId",
                table: "ItemModels");
        }
    }
}
