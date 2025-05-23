﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ScheduleBackend.Db;

#nullable disable

namespace ScheduleBackend.Migrations
{
    [DbContext(typeof(ScheduleDbContext))]
    [Migration("20250519122221_MakeAdminIdNullableInRegistration")]
    partial class MakeAdminIdNullableInRegistration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Admin", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Registration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AdminId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Schedule", b =>
                {
                    b.Property<Guid>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.HasKey("ScheduleId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<Guid>("ScheduleId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId")
                        .IsUnique();

                    b.ToTable("Students");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ActiveSlots")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Teachers");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.TeacherSchedule", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeacherSchedule");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Registration", b =>
                {
                    b.HasOne("ScheduleBackend.Models.Entity.Admin", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Schedule", b =>
                {
                    b.OwnsMany("ScheduleBackend.Models.Entity.DaySchedule", "Days", b1 =>
                        {
                            b1.Property<Guid>("ScheduleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("DayNumber")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("DayNumber"));

                            b1.HasKey("ScheduleId", "DayNumber");

                            b1.ToTable("DaySchedule");

                            b1.WithOwner()
                                .HasForeignKey("ScheduleId");

                            b1.OwnsMany("ScheduleBackend.Models.Entity.Activity", "Activities", b2 =>
                                {
                                    b2.Property<Guid>("ScheduleId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("DayNumber")
                                        .HasColumnType("integer");

                                    b2.Property<int>("ActivityNumber")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b2.Property<int>("ActivityNumber"));

                                    b2.Property<DateTime>("EndTime")
                                        .HasColumnType("timestamp without time zone");

                                    b2.Property<bool>("IsBooked")
                                        .HasColumnType("boolean");

                                    b2.Property<string>("LessonName")
                                        .HasMaxLength(100)
                                        .HasColumnType("character varying(100)");

                                    b2.Property<DateTime>("StartTime")
                                        .HasColumnType("timestamp without time zone");

                                    b2.HasKey("ScheduleId", "DayNumber", "ActivityNumber");

                                    b2.ToTable("Activity");

                                    b2.WithOwner()
                                        .HasForeignKey("ScheduleId", "DayNumber");
                                });

                            b1.Navigation("Activities");
                        });

                    b.Navigation("Days");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Student", b =>
                {
                    b.HasOne("ScheduleBackend.Models.Entity.Schedule", "Schedule")
                        .WithOne("User")
                        .HasForeignKey("ScheduleBackend.Models.Entity.Student", "ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Schedule");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.TeacherSchedule", b =>
                {
                    b.HasOne("ScheduleBackend.Models.Entity.Teacher", "Teacher")
                        .WithMany("TeacherSchedules")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("ScheduleBackend.Models.Entity.Lesson", "Lessons", b1 =>
                        {
                            b1.Property<Guid>("TeacherScheduleId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<int>("ActivityNumber")
                                .HasColumnType("integer");

                            b1.Property<int>("DayNumber")
                                .HasColumnType("integer");

                            b1.Property<DateTime>("EndTime")
                                .HasColumnType("timestamp without time zone");

                            b1.Property<string>("LessonName")
                                .HasMaxLength(100)
                                .HasColumnType("character varying(100)");

                            b1.Property<DateTime>("StartTime")
                                .HasColumnType("timestamp without time zone");

                            b1.HasKey("TeacherScheduleId", "Id");

                            b1.ToTable("Lessons");

                            b1.WithOwner()
                                .HasForeignKey("TeacherScheduleId");
                        });

                    b.Navigation("Lessons");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Schedule", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("ScheduleBackend.Models.Entity.Teacher", b =>
                {
                    b.Navigation("TeacherSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
