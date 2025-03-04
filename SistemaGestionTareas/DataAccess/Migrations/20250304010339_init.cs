using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password" },
                values: new object[] { 1, "prueba@prueba.com", "Abcd1234_" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "Description", "ExpirationDate", "Status", "Title", "UserId" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.", new DateOnly(2025, 3, 4), 2, "Prueba 1", 1 },
                    { 2, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.", new DateOnly(2025, 3, 21), 0, "Prueba 2", 1 },
                    { 3, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.", new DateOnly(2025, 4, 3), 0, "Prueba 3", 1 },
                    { 4, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.", new DateOnly(2025, 3, 13), 1, "Prueba 4", 1 },
                    { 5, "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.", new DateOnly(2025, 6, 3), 0, "Prueba 5", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                table: "Tasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
