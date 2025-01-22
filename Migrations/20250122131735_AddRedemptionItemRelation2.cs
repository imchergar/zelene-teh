using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class AddRedemptionItemRelation2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemModels_RedemptionModels_RedemptionId",
                table: "ItemModels");

            migrationBuilder.AlterColumn<int>(
                name: "RedemptionId",
                table: "ItemModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemModels_RedemptionModels_RedemptionId",
                table: "ItemModels",
                column: "RedemptionId",
                principalTable: "RedemptionModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemModels_RedemptionModels_RedemptionId",
                table: "ItemModels");

            migrationBuilder.AlterColumn<int>(
                name: "RedemptionId",
                table: "ItemModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemModels_RedemptionModels_RedemptionId",
                table: "ItemModels",
                column: "RedemptionId",
                principalTable: "RedemptionModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
