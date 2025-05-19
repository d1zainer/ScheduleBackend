using System.Data;
using Microsoft.EntityFrameworkCore;
using ScheduleBackend.Models.Entity;

namespace ScheduleBackend.Db
{
    public class ScheduleDbContext : DbContext
    {
        public ScheduleDbContext(DbContextOptions<ScheduleDbContext> options)
            : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Schedule> Schedules => Set<Schedule>();
        public DbSet<Lesson> Lessons => Set<Lesson>();
        public DbSet<Registration> Registrations => Set<Registration>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("timestamp without time zone");
            });
            // Конфигурация Schedule -> Days -> Activities
            modelBuilder.Entity<Schedule>(scheduleBuilder =>
            {
                scheduleBuilder.HasKey(s => s.ScheduleId);

                scheduleBuilder.OwnsMany(s => s.Days, dayBuilder =>
                {
                    dayBuilder.WithOwner().HasForeignKey("ScheduleId");
                    dayBuilder.HasKey("ScheduleId", "DayNumber"); // Добавили составной ключ

                    dayBuilder.OwnsMany(d => d.Activities, activityBuilder =>
                    {
                        activityBuilder.WithOwner().HasForeignKey("ScheduleId", "DayNumber");
                        activityBuilder.HasKey("ScheduleId", "DayNumber", "ActivityNumber"); // Ключ из 3 полей

                        // Можно добавить свойства если нужно
                        activityBuilder.Property(a => a.ActivityNumber);
                        activityBuilder.Property(a => a.StartTime).HasColumnType("timestamp without time zone"); ;
                        activityBuilder.Property(a => a.EndTime).HasColumnType("timestamp without time zone"); ;
                        activityBuilder.Property(a => a.IsBooked);
                        activityBuilder.Property(a => a.LessonName);
                    });
                });
            });


            // Конфигурация TeacherSchedule -> Lessons (owned type)
            modelBuilder.Entity<TeacherSchedule>(teacherScheduleBuilder =>
            {
                teacherScheduleBuilder.HasKey(ts => ts.Id);

                teacherScheduleBuilder.HasOne(ts => ts.Teacher)
                    .WithMany(t => t.TeacherSchedules)
                    .HasForeignKey(ts => ts.TeacherId);

                teacherScheduleBuilder.OwnsMany(ts => ts.Lessons, lessonBuilder =>
                {
                    lessonBuilder.WithOwner().HasForeignKey("TeacherScheduleId");

                    lessonBuilder.Property(l => l.DayNumber).IsRequired();
                    lessonBuilder.Property(l => l.ActivityNumber).IsRequired();
                    lessonBuilder.Property(l => l.StartTime)  .HasColumnType("timestamp without time zone"). IsRequired();
                    lessonBuilder.Property(l => l.EndTime)   .HasColumnType("timestamp without time zone") .IsRequired();
                    lessonBuilder.Property(l => l.LessonName).HasMaxLength(100);

                    // НЕ вызывай HasNoKey() здесь!
                });
            });
       


            // Если есть другие сущности, их конфигурацию добавляй здесь...
        }




    }
}