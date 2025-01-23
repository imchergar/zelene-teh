using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class ItemRedemptionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemRedemptionModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemModelId = table.Column<int>(type: "int", nullable: false),
                    RedemptionModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemRedemptionModels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemRedemptionModels_ItemModels_ItemModelId",
                        column: x => x.ItemModelId,
                        principalTable: "ItemModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemRedemptionModels_RedemptionModels_RedemptionModelId",
                        column: x => x.RedemptionModelId,
                        principalTable: "RedemptionModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemRedemptionModels_ItemModelId",
                table: "ItemRedemptionModels",
                column: "ItemModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemRedemptionModels_RedemptionModelId",
                table: "ItemRedemptionModels",
                column: "RedemptionModelId");
            
            migrationBuilder.DropForeignKey(
                name: "FK_ItemModels_RedemptionModels_RedemptionModelId",
                table: "ItemModels");

            migrationBuilder.DropIndex(
                name: "IX_ItemModels_RedemptionModelId",
                table: "ItemModels");

            migrationBuilder.DropColumn(
                name: "RedemptionModelId",
                table: "ItemModels");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RedemptionModelId",
                table: "ItemModels",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemModels_RedemptionModelId",
                table: "ItemModels",
                column: "RedemptionModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemModels_RedemptionModels_RedemptionModelId",
                table: "ItemModels",
                column: "RedemptionModelId",
                principalTable: "RedemptionModels",
                principalColumn: "Id");
            
            migrationBuilder.DropTable(
                name: "ItemRedemptionModels");
        }
    }
}
