using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Infrastructure.DataAccess.Migrations
{
    public partial class BookFormat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookFormatValue",
                table: "Books",
                nullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookFormats_BookFormatValue",
                table: "Books",
                column: "BookFormatValue",
                principalTable: "BookFormats",
                principalColumn: "Value",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookFormats_BookFormatValue",
                table: "Books");

            migrationBuilder.DropTable(
                name: "BookFormats");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookFormatValue",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookFormatValue",
                table: "Books");
        }
    }
}
