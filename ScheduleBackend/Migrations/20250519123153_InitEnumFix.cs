using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitEnumFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Добавляем временное поле типа int
            migrationBuilder.AddColumn<int>(
                name: "StatusTmp",
                table: "Registrations",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            // Копируем значения из старого string Status в новый int StatusTmp
            migrationBuilder.Sql(@"
        UPDATE ""Registrations"" SET ""StatusTmp"" = 
            CASE ""Status""
                WHEN 'New' THEN 0
                WHEN 'InProgress' THEN 1
                WHEN 'Done' THEN 2
                WHEN 'Rejected' THEN 3
                ELSE 0
            END;
    ");

            // Удаляем старое поле
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Registrations");

            // Переименовываем новое поле
            migrationBuilder.RenameColumn(
                name: "StatusTmp",
                table: "Registrations",
                newName: "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Добавляем временное поле string
            migrationBuilder.AddColumn<string>(
                name: "StatusOld",
                table: "Registrations",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "New");

            // Копируем значения из int Status обратно в string StatusOld
            migrationBuilder.Sql(@"
        UPDATE ""Registrations"" SET ""StatusOld"" = 
            CASE ""Status""
                WHEN 0 THEN 'New'
                WHEN 1 THEN 'InProgress'
                WHEN 2 THEN 'Done'
                WHEN 3 THEN 'Rejected'
                ELSE 'New'
            END;
    ");

            // Удаляем int Status
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Registrations");

            // Переименовываем обратно в Status
            migrationBuilder.RenameColumn(
                name: "StatusOld",
                table: "Registrations",
                newName: "Status");
        }

    }
}
