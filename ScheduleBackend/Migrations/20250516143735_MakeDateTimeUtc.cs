using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScheduleBackend.Migrations
{
    public partial class MakeDateTimeUtc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE public.""Activity""
                    ALTER COLUMN ""StartTime"" TYPE timestamp without time zone
                    USING ""StartTime"" AT TIME ZONE 'UTC';
                
                ALTER TABLE public.""Activity""
                    ALTER COLUMN ""EndTime"" TYPE timestamp without time zone
                    USING ""EndTime"" AT TIME ZONE 'UTC';

                ALTER TABLE public.""Lessons""
                    ALTER COLUMN ""StartTime"" TYPE timestamp without time zone
                    USING ""StartTime"" AT TIME ZONE 'UTC';

                ALTER TABLE public.""Lessons""
                    ALTER COLUMN ""EndTime"" TYPE timestamp without time zone
                    USING ""EndTime"" AT TIME ZONE 'UTC';

                ALTER TABLE public.""Registrations""
                    ALTER COLUMN ""CreatedAt"" TYPE timestamp without time zone
                    USING ""CreatedAt"" AT TIME ZONE 'UTC';

                ALTER TABLE public.""Users""
                    ALTER COLUMN ""DateOfBirth"" TYPE timestamp without time zone
                    USING ""DateOfBirth"" AT TIME ZONE 'UTC';
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE public.""Activity""
                    ALTER COLUMN ""StartTime"" TYPE timestamp with time zone
                    USING ""StartTime"" AT TIME ZONE 'UTC';
                
                ALTER TABLE public.""Activity""
                    ALTER COLUMN ""EndTime"" TYPE timestamp with time zone
                    USING ""EndTime"" AT TIME ZONE 'UTC';

                ALTER TABLE public.""Lessons""
                    ALTER COLUMN ""StartTime"" TYPE timestamp with time zone
                    USING ""StartTime"" AT TIME ZONE 'UTC';

                ALTER TABLE public.""Lessons""
                    ALTER COLUMN ""EndTime"" TYPE timestamp with time zone
                    USING ""EndTime"" AT TIME ZONE 'UTC';

                ALTER TABLE public.""Registrations""
                    ALTER COLUMN ""CreatedAt"" TYPE timestamp with time zone
                    USING ""CreatedAt"" AT TIME ZONE 'UTC';

                ALTER TABLE public.""Users""
                    ALTER COLUMN ""DateOfBirth"" TYPE timestamp with time zone
                    USING ""DateOfBirth"" AT TIME ZONE 'UTC';
            ");
        }
    }
}
