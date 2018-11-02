using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FinalProject_Dips2.Migrations
{
    public partial class Init_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblCategories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TblImages",
                columns: table => new
                {
                    ImageId = table.Column<Guid>(nullable: false),
                    ContentType = table.Column<string>(nullable: true),
                    Data = table.Column<byte[]>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    Height = table.Column<int>(nullable: false),
                    Length = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblImages", x => x.ImageId);
                });

            migrationBuilder.CreateTable(
                name: "TblProduct",
                columns: table => new
                {
                    ProductId = table.Column<Guid>(nullable: false),
                    CostPrice = table.Column<decimal>(nullable: false),
                    ProductName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProduct", x => x.ProductId);
                });

            migrationBuilder.CreateTable(
                name: "TblHamper",
                columns: table => new
                {
                    HamperId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    HamperName = table.Column<string>(nullable: true),
                    ImageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHamper", x => x.HamperId);
                    table.ForeignKey(
                        name: "FK_TblHamper_TblCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "TblCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblHamper_TblImages_ImageId",
                        column: x => x.ImageId,
                        principalTable: "TblImages",
                        principalColumn: "ImageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblHamperProducts",
                columns: table => new
                {
                    HamperId = table.Column<Guid>(nullable: false),
                    ProductId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblHamperProducts", x => new { x.HamperId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_TblHamperProducts_TblHamper_HamperId",
                        column: x => x.HamperId,
                        principalTable: "TblHamper",
                        principalColumn: "HamperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblHamperProducts_TblProduct_ProductId",
                        column: x => x.ProductId,
                        principalTable: "TblProduct",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_CategoryId",
                table: "TblHamper",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamper_ImageId",
                table: "TblHamper",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_TblHamperProducts_ProductId",
                table: "TblHamperProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblHamperProducts");

            migrationBuilder.DropTable(
                name: "TblHamper");

            migrationBuilder.DropTable(
                name: "TblProduct");

            migrationBuilder.DropTable(
                name: "TblCategories");

            migrationBuilder.DropTable(
                name: "TblImages");
        }
    }
}
