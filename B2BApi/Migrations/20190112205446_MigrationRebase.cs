using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace B2BApi.Migrations
{
    public partial class MigrationRebase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopCategoryId",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopCategoryId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeRange",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRange", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BrandTypes_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopBrandId",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeName = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopBrandId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopBrandId_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Parent = table.Column<int>(nullable: false),
                    ShopCategoryIdId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_ShopCategoryId_ShopCategoryIdId",
                        column: x => x.ShopCategoryIdId,
                        principalTable: "ShopCategoryId",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProviderType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Bank = table.Column<string>(nullable: true),
                    KorSchet = table.Column<string>(nullable: true),
                    RasSchet = table.Column<string>(nullable: true),
                    Bic = table.Column<string>(nullable: true),
                    Inn = table.Column<string>(nullable: true),
                    uAddress = table.Column<string>(nullable: true),
                    OfficeWorkTimeRangeId = table.Column<int>(nullable: true),
                    StockWorkTimeRangeId = table.Column<int>(nullable: true),
                    StockAddress = table.Column<string>(nullable: true),
                    RequestDeadline = table.Column<DateTime>(nullable: false),
                    TimeOfDelivery = table.Column<DateTime>(nullable: false),
                    Currency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Providers_TimeRange_OfficeWorkTimeRangeId",
                        column: x => x.OfficeWorkTimeRangeId,
                        principalTable: "TimeRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Providers_TimeRange_StockWorkTimeRangeId",
                        column: x => x.StockWorkTimeRangeId,
                        principalTable: "TimeRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Model = table.Column<string>(nullable: true),
                    BrandId = table.Column<int>(nullable: true),
                    BrandTypeId = table.Column<int>(nullable: true),
                    PartNumber = table.Column<string>(nullable: true),
                    Gtin = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    ProducerUri = table.Column<string>(nullable: true),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_BrandTypes_BrandTypeId",
                        column: x => x.BrandTypeId,
                        principalTable: "BrandTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Handlers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    SaveFileName = table.Column<string>(nullable: true),
                    StartRowData = table.Column<int>(nullable: false),
                    ProviderId = table.Column<int>(nullable: true),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Handlers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Handlers_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProviderContact",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProviderType = table.Column<int>(nullable: false),
                    TelephoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ProviderId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderContact", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderContact_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Type = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attributes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attributes_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorsPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompetitorId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorsPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorsPrices_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorsPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompetitorsUri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompetitorId = table.Column<int>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompetitorsUri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompetitorsUri_Competitors_CompetitorId",
                        column: x => x.CompetitorId,
                        principalTable: "Competitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompetitorsUri_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Price",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PriceType = table.Column<int>(nullable: false),
                    Value = table.Column<double>(nullable: false),
                    ProductId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Price", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Price_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProductId = table.Column<int>(nullable: true),
                    ProviderId = table.Column<int>(nullable: true),
                    Count = table.Column<int>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockProducts_Providers_ProviderId",
                        column: x => x.ProviderId,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GrabColumnItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GrabColumn = table.Column<int>(nullable: false),
                    Value = table.Column<byte>(nullable: false),
                    HandlerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrabColumnItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GrabColumnItem_Handlers_HandlerId",
                        column: x => x.HandlerId,
                        principalTable: "Handlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HandlerSettings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Field = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    HandlerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HandlerSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HandlerSettings_Handlers_HandlerId",
                        column: x => x.HandlerId,
                        principalTable: "Handlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pattern",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ColumnId = table.Column<int>(nullable: false),
                    Old = table.Column<string>(nullable: true),
                    New = table.Column<string>(nullable: true),
                    HandlerId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pattern", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pattern_Handlers_HandlerId",
                        column: x => x.HandlerId,
                        principalTable: "Handlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_CategoryId",
                table: "Attributes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Attributes_ProductId",
                table: "Attributes",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BrandTypes_BrandId",
                table: "BrandTypes",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ShopCategoryIdId",
                table: "Categories",
                column: "ShopCategoryIdId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorsPrices_CompetitorId",
                table: "CompetitorsPrices",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorsPrices_ProductId",
                table: "CompetitorsPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorsUri_CompetitorId",
                table: "CompetitorsUri",
                column: "CompetitorId");

            migrationBuilder.CreateIndex(
                name: "IX_CompetitorsUri_ProductId",
                table: "CompetitorsUri",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_GrabColumnItem_HandlerId",
                table: "GrabColumnItem",
                column: "HandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Handlers_ProviderId",
                table: "Handlers",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_HandlerSettings_HandlerId",
                table: "HandlerSettings",
                column: "HandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Pattern_HandlerId",
                table: "Pattern",
                column: "HandlerId");

            migrationBuilder.CreateIndex(
                name: "IX_Price_ProductId",
                table: "Price",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandTypeId",
                table: "Products",
                column: "BrandTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderContact_ProviderId",
                table: "ProviderContact",
                column: "ProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_OfficeWorkTimeRangeId",
                table: "Providers",
                column: "OfficeWorkTimeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Providers_StockWorkTimeRangeId",
                table: "Providers",
                column: "StockWorkTimeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopBrandId_BrandId",
                table: "ShopBrandId",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_StockProducts_ProductId",
                table: "StockProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_StockProducts_ProviderId",
                table: "StockProducts",
                column: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "CompetitorsPrices");

            migrationBuilder.DropTable(
                name: "CompetitorsUri");

            migrationBuilder.DropTable(
                name: "GrabColumnItem");

            migrationBuilder.DropTable(
                name: "HandlerSettings");

            migrationBuilder.DropTable(
                name: "Pattern");

            migrationBuilder.DropTable(
                name: "Price");

            migrationBuilder.DropTable(
                name: "ProviderContact");

            migrationBuilder.DropTable(
                name: "ShopBrandId");

            migrationBuilder.DropTable(
                name: "StockProducts");

            migrationBuilder.DropTable(
                name: "Competitors");

            migrationBuilder.DropTable(
                name: "Handlers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "BrandTypes");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "TimeRange");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "ShopCategoryId");
        }
    }
}
