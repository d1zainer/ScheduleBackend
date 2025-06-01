using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleBackend.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Admins_AdminId",
                table: "Registrations");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Admins_AdminId",
                table: "Registrations",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Registrations_Admins_AdminId",
                table: "Registrations");

            migrationBuilder.AddForeignKey(
                name: "FK_Registrations_Admins_AdminId",
                table: "Registrations",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }
    }
}
