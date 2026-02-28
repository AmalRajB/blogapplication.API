using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blogapplication.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogpostCategory",
                columns: table => new
                {
                    BlogpostsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    categoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogpostCategory", x => new { x.BlogpostsId, x.categoriesId });
                    table.ForeignKey(
                        name: "FK_BlogpostCategory_Blogposts_BlogpostsId",
                        column: x => x.BlogpostsId,
                        principalTable: "Blogposts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogpostCategory_Categories_categoriesId",
                        column: x => x.categoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogpostCategory_categoriesId",
                table: "BlogpostCategory",
                column: "categoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogpostCategory");
        }
    }
}
