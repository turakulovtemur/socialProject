using Microsoft.EntityFrameworkCore.Migrations;

namespace socialProject.Migrations
{
    public partial class InitialUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Snils",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Pasports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Snils_UserId",
                table: "Snils",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pasports_UserId",
                table: "Pasports",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pasports_AspNetUsers_UserId",
                table: "Pasports",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Snils_AspNetUsers_UserId",
                table: "Snils",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pasports_AspNetUsers_UserId",
                table: "Pasports");

            migrationBuilder.DropForeignKey(
                name: "FK_Snils_AspNetUsers_UserId",
                table: "Snils");

            migrationBuilder.DropIndex(
                name: "IX_Snils_UserId",
                table: "Snils");

            migrationBuilder.DropIndex(
                name: "IX_Pasports_UserId",
                table: "Pasports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Snils");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pasports");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PasportId = table.Column<int>(type: "int", nullable: false),
                    SnilsId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Persons_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Persons_Pasports_PasportId",
                        column: x => x.PasportId,
                        principalTable: "Pasports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Persons_Snils_SnilsId",
                        column: x => x.SnilsId,
                        principalTable: "Snils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persons_PasportId",
                table: "Persons",
                column: "PasportId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_SnilsId",
                table: "Persons",
                column: "SnilsId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_UserId1",
                table: "Persons",
                column: "UserId1");
        }
    }
}
