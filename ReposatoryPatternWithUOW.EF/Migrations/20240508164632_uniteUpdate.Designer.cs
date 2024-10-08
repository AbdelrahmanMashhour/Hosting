﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RepositoryPatternWithUOW.EF;

#nullable disable

namespace RepositoryPatternWithUOW.EF.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240508164632_uniteUpdate")]
    partial class uniteUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AssignmentId"));

                    b.Property<string>("AssFiles")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FullMark")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UniteId")
                        .HasColumnType("int");

                    b.HasKey("AssignmentId");

                    b.HasIndex("UniteId")
                        .IsUnique();

                    b.ToTable("Assignments");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<int>("AdminId")
                        .HasColumnType("int");

                    b.Property<string>("CoursStage")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CoursePrice")
                        .HasColumnType("int");

                    b.Property<string>("ProfileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TotoalHoure")
                        .HasColumnType("int");

                    b.HasKey("CourseId");

                    b.HasIndex("AdminId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.EmailVerificationCode", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Code")
                        .HasMaxLength(10)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "Code");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("EmailVerificationCode");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.IdentityTokenVerification", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "Token");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("IdentityTokenVerifications");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.RefreshToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasMaxLength(44)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Revoked")
                        .HasColumnType("bit");

                    b.HasKey("UserId", "Token");

                    b.ToTable("RefreshToken");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Solution", b =>
                {
                    b.Property<int>("AssignmentId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("SolutionFileUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("StudentDegree")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("AssignmentId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Solutions");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.StudentCourse", b =>
                {
                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<DateOnly>("JoinedAt")
                        .HasColumnType("date");

                    b.HasKey("CourseId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.StudentPhones", b =>
                {
                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<string>("Phone")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("DadPhone")
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("StudentId", "Phone", "DadPhone");

                    b.ToTable("StudentPhones");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Unite", b =>
                {
                    b.Property<int>("UnitId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("ExamUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkillPdfUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SkillUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoryPdfUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoryUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TranslationPdfUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TranslationUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VocablaryPdfUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VocablaryUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitId");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.ToTable("Unites");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool>("Blocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ProfilePictureUrl")
                        .HasColumnType("nvarchar(max)");

                    SqlServerPropertyBuilderExtensions.IsSparse(b.Property<string>("ProfilePictureUrl"));

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)")
                        .HasDefaultValue("Student");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Role").HasValue("User");

                    b.UseTphMappingStrategy();

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Blocked = false,
                            Email = "theknightahmedgaber@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "The Knight",
                            LastName = "Platform",
                            Password = "$2a$11$oaB0l7afGUkezbjnOOOvA.ZQgzfP0BZn5mAFUwYKHZWyvlgsktZUG",
                            Role = "Admin"
                        });
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Admin", b =>
                {
                    b.HasBaseType("RepositoryPatternWithUOW.Core.Models.User");

                    b.HasDiscriminator().HasValue("Admin");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Student", b =>
                {
                    b.HasBaseType("RepositoryPatternWithUOW.Core.Models.User");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Grade")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stages")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Assignment", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.Unite", "Unite")
                        .WithOne("Assignment")
                        .HasForeignKey("RepositoryPatternWithUOW.Core.Models.Assignment", "UniteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Unite");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Course", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.Admin", "Admin")
                        .WithMany("Courses")
                        .HasForeignKey("AdminId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Admin");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.EmailVerificationCode", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.User", "User")
                        .WithOne("EmailVerificationCode")
                        .HasForeignKey("RepositoryPatternWithUOW.Core.Models.EmailVerificationCode", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.IdentityTokenVerification", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.User", "User")
                        .WithOne("IdentityTokenVerification")
                        .HasForeignKey("RepositoryPatternWithUOW.Core.Models.IdentityTokenVerification", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.RefreshToken", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Solution", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.Assignment", "Assignment")
                        .WithMany("Solutions")
                        .HasForeignKey("AssignmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepositoryPatternWithUOW.Core.Models.Student", "Student")
                        .WithMany("Solution")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assignment");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.StudentCourse", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RepositoryPatternWithUOW.Core.Models.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.StudentPhones", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.Student", "Student")
                        .WithMany("StudentPhones")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Unite", b =>
                {
                    b.HasOne("RepositoryPatternWithUOW.Core.Models.Course", "Course")
                        .WithOne("Unite")
                        .HasForeignKey("RepositoryPatternWithUOW.Core.Models.Unite", "CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Assignment", b =>
                {
                    b.Navigation("Solutions");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Course", b =>
                {
                    b.Navigation("StudentCourses");

                    b.Navigation("Unite")
                        .IsRequired();
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Unite", b =>
                {
                    b.Navigation("Assignment")
                        .IsRequired();
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.User", b =>
                {
                    b.Navigation("EmailVerificationCode")
                        .IsRequired();

                    b.Navigation("IdentityTokenVerification")
                        .IsRequired();

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Admin", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("RepositoryPatternWithUOW.Core.Models.Student", b =>
                {
                    b.Navigation("Solution");

                    b.Navigation("StudentCourses");

                    b.Navigation("StudentPhones");
                });
#pragma warning restore 612, 618
        }
    }
}
