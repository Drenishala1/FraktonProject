using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FraktonProject.Migrations
{
    public partial class addRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BelongsToUserId = table.Column<int>(type: "int", nullable: true),
                    BelongsToUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RevokedByTokenId = table.Column<int>(type: "int", nullable: true),
                    RevokedByUserId = table.Column<int>(type: "int", nullable: true),
                    RevokedByUserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RevokedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_BelongsToUserId1",
                        column: x => x.BelongsToUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_RevokedByUserId1",
                        column: x => x.RevokedByUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_RefreshTokens_RevokedByTokenId",
                        column: x => x.RevokedByTokenId,
                        principalTable: "RefreshTokens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_BelongsToUserId1",
                table: "RefreshTokens",
                column: "BelongsToUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_RevokedByTokenId",
                table: "RefreshTokens",
                column: "RevokedByTokenId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_RevokedByUserId1",
                table: "RefreshTokens",
                column: "RevokedByUserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshTokens");
        }
    }
}
