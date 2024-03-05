using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CS68_MVC1.Migrations
{
    /// <inheritdoc />
    public partial class UserClaim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoleClaim_users_UserID",
                table: "RoleClaim");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoleClaim",
                table: "RoleClaim");

            migrationBuilder.RenameTable(
                name: "RoleClaim",
                newName: "roleClaims");

            migrationBuilder.AddPrimaryKey(
                name: "PK_roleClaims",
                table: "roleClaims",
                columns: new[] { "UserID", "ClaimTypes", "ClaimName" });

            migrationBuilder.AddForeignKey(
                name: "FK_roleClaims_users_UserID",
                table: "roleClaims",
                column: "UserID",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_roleClaims_users_UserID",
                table: "roleClaims");

            migrationBuilder.DropPrimaryKey(
                name: "PK_roleClaims",
                table: "roleClaims");

            migrationBuilder.RenameTable(
                name: "roleClaims",
                newName: "RoleClaim");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoleClaim",
                table: "RoleClaim",
                columns: new[] { "UserID", "ClaimTypes", "ClaimName" });

            migrationBuilder.AddForeignKey(
                name: "FK_RoleClaim_users_UserID",
                table: "RoleClaim",
                column: "UserID",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
