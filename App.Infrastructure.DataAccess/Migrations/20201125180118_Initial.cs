using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.DataAccess.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookFormats",
                columns: table => new
                {
                    Value = table.Column<int>(nullable: false, defaultValue: 1),
                    DisplayName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookFormats", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 256, nullable: false),
                    Author = table.Column<string>(maxLength: 256, nullable: false),
                    BookFormatValue = table.Column<int>(nullable: true),
                    PublishedDate = table.Column<DateTime>(nullable: true),
                    LikeCount = table.Column<int>(nullable: false),
                    DislikeCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_Books_BookFormats_BookFormatValue",
                        column: x => x.BookFormatValue,
                        principalTable: "BookFormats",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(maxLength: 1024, nullable: false),
                    EmailAddress = table.Column<string>(maxLength: 128, nullable: false),
                    Flagged = table.Column<bool>(nullable: false),
                    CreatedDateTime = table.Column<DateTime>(nullable: false),
                    BookId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                    table.ForeignKey(
                        name: "FK_Reviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BookFormats",
                columns: new[] { "Value", "DisplayName" },
                values: new object[] { 0, "Book" });

            migrationBuilder.InsertData(
                table: "BookFormats",
                columns: new[] { "Value", "DisplayName" },
                values: new object[] { 1, "AudioBook" });

            migrationBuilder.InsertData(
                table: "BookFormats",
                columns: new[] { "Value", "DisplayName" },
                values: new object[] { 2, "eBook" });

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookFormatValue",
                table: "Books",
                column: "BookFormatValue");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "BookFormats");
        }
    }
}
