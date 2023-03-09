using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    acc_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    acc_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_birth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_image_base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_history = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_status = table.Column<int>(type: "int", nullable: false),
                    acc_type = table.Column<int>(type: "int", nullable: false),
                    acc_deli_address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    acc_ip = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.acc_id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    cate_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cate_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cate_status = table.Column<bool>(type: "bit", nullable: false),
                    sub_cate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.cate_id);
                });

            migrationBuilder.CreateTable(
                name: "Producer",
                columns: table => new
                {
                    pro_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pro_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro_status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producer", x => x.pro_id);
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    b_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created_time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    b_total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    b_status = table.Column<int>(type: "int", nullable: false),
                    acc_id = table.Column<int>(type: "int", nullable: false),
                    AccId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.b_id);
                    table.ForeignKey(
                        name: "FK_Bill_Account_AccId",
                        column: x => x.AccId,
                        principalTable: "Account",
                        principalColumn: "acc_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    p_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    p_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    imported_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_imported_quantity = table.Column<int>(type: "int", nullable: false),
                    exported_at = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_exported_quantity = table.Column<int>(type: "int", nullable: false),
                    cmt_count = table.Column<int>(type: "int", nullable: false),
                    like_count = table.Column<int>(type: "int", nullable: false),
                    dislike_count = table.Column<int>(type: "int", nullable: false),
                    p_status = table.Column<int>(type: "int", nullable: false),
                    p_thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_thumbnail_base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img1_base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img2_base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img3_base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img4 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img4_base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img5 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    p_img5_base64 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cate_id = table.Column<int>(type: "int", nullable: false),
                    CateId = table.Column<int>(type: "int", nullable: false),
                    acc_id = table.Column<int>(type: "int", nullable: false),
                    AccId = table.Column<int>(type: "int", nullable: false),
                    pro_id = table.Column<int>(type: "int", nullable: false),
                    ProId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.p_id);
                    table.ForeignKey(
                        name: "FK_Product_Account_AccId",
                        column: x => x.AccId,
                        principalTable: "Account",
                        principalColumn: "acc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Category_CateId",
                        column: x => x.CateId,
                        principalTable: "Category",
                        principalColumn: "cate_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Producer_ProId",
                        column: x => x.ProId,
                        principalTable: "Producer",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetail",
                columns: table => new
                {
                    bid_id = table.Column<int>(type: "int", nullable: false),
                    b_id = table.Column<int>(type: "int", nullable: false),
                    p_id = table.Column<int>(type: "int", nullable: false),
                    bid_amount = table.Column<int>(type: "int", nullable: false),
                    bid_payment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BillId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetail", x => new { x.bid_id, x.b_id, x.p_id });
                    table.ForeignKey(
                        name: "FK_BillDetail_Bill_BillId",
                        column: x => x.BillId,
                        principalTable: "Bill",
                        principalColumn: "b_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetail_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "p_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    cmt_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cmt_content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cmt_count = table.Column<int>(type: "int", nullable: false),
                    like_count = table.Column<int>(type: "int", nullable: false),
                    dislike_count = table.Column<int>(type: "int", nullable: false),
                    rep_cmt_id = table.Column<int>(type: "int", nullable: false),
                    p_id = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    acc_id = table.Column<int>(type: "int", nullable: false),
                    AccId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.cmt_id);
                    table.ForeignKey(
                        name: "FK_Comment_Account_AccId",
                        column: x => x.AccId,
                        principalTable: "Account",
                        principalColumn: "acc_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comment_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "p_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_AccId",
                table: "Bill",
                column: "AccId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_BillId",
                table: "BillDetail",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetail_ProductId",
                table: "BillDetail",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_AccId",
                table: "Comment",
                column: "AccId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ProductId",
                table: "Comment",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_AccId",
                table: "Product",
                column: "AccId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_CateId",
                table: "Product",
                column: "CateId");

            migrationBuilder.CreateIndex(
                name: "IX_Product_ProId",
                table: "Product",
                column: "ProId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetail");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Producer");
        }
    }
}
