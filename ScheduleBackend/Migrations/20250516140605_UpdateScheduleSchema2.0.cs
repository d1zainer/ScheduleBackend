using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdateScheduleSchema20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_DaySchedule_DayNumber",
                table: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaySchedule",
                table: "DaySchedule");

            migrationBuilder.DropIndex(
                name: "IX_DaySchedule_ScheduleId",
                table: "DaySchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activity",
                table: "Activity");

            migrationBuilder.DropIndex(
                name: "IX_Activity_DayNumber",
                table: "Activity");

            migrationBuilder.AddColumn<Guid>(
                name: "ScheduleId",
                table: "Activity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaySchedule",
                table: "DaySchedule",
                columns: new[] { "ScheduleId", "DayNumber" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activity",
                table: "Activity",
                columns: new[] { "ScheduleId", "DayNumber", "ActivityNumber" });

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_DaySchedule_ScheduleId_DayNumber",
                table: "Activity",
                columns: new[] { "ScheduleId", "DayNumber" },
                principalTable: "DaySchedule",
                principalColumns: new[] { "ScheduleId", "DayNumber" },
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activity_DaySchedule_ScheduleId_DayNumber",
                table: "Activity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DaySchedule",
                table: "DaySchedule");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Activity",
                table: "Activity");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Activity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DaySchedule",
                table: "DaySchedule",
                column: "DayNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Activity",
                table: "Activity",
                column: "ActivityNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DaySchedule_ScheduleId",
                table: "DaySchedule",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Activity_DayNumber",
                table: "Activity",
                column: "DayNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Activity_DaySchedule_DayNumber",
                table: "Activity",
                column: "DayNumber",
                principalTable: "DaySchedule",
                principalColumn: "DayNumber",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
