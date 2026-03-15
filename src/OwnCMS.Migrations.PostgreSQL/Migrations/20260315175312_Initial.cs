using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwnCMS.Migrations.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "article",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "article_content",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    article_id = table.Column<Guid>(type: "uuid", nullable: false),
                    html = table.Column<string>(type: "text", nullable: false),
                    css = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_content", x => x.id);
                    table.ForeignKey(
                        name: "FK_article_content_article_article_id",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "article_category",
                columns: table => new
                {
                    article_id = table.Column<Guid>(type: "uuid", nullable: false),
                    category_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_article_category", x => new { x.article_id, x.category_id });
                    table.ForeignKey(
                        name: "article_id_fk",
                        column: x => x.article_id,
                        principalTable: "article",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "category_id_fk",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_article_name",
                table: "article",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_article_slug",
                table: "article",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_article_category_category_id",
                table: "article_category",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_article_content_article_id",
                table: "article_content",
                column: "article_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_name",
                table: "category",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_category_slug",
                table: "category",
                column: "slug",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "article_category");

            migrationBuilder.DropTable(
                name: "article_content");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "article");
        }
    }
}
