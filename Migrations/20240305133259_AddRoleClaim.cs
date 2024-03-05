using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS68_MVC1.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleClaim",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    ClaimTypes = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaim", x => new { x.UserID, x.ClaimTypes, x.ClaimName });
                    table.ForeignKey(
                        name: "FK_RoleClaim_users_UserID",
                        column: x => x.UserID,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaim");
        }
    }
}
