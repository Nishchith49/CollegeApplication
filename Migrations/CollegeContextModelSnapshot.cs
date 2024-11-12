﻿// <auto-generated />
using System;
using CollegeApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CollegeApplication.Migrations
{
    [DbContext(typeof(CollegeContext))]
    partial class CollegeContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("CollegeApplication.Entities.College", b =>
                {
                    b.Property<long>("CollegeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint(20)")
                        .HasColumnName("College_Id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("CollegeId"));

                    b.Property<string>("Dean")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Dean");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Location");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.HasKey("CollegeId");

                    b.ToTable("Colleges");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Course", b =>
                {
                    b.Property<long>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Course_Id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("CourseId"));

                    b.Property<long>("Credits")
                        .HasColumnType("bigint")
                        .HasColumnName("Credits");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("Department_Id");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(500)")
                        .HasColumnName("Description");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Title");

                    b.HasKey("CourseId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Department", b =>
                {
                    b.Property<long>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Department_Id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("DepartmentId"));

                    b.Property<long>("CollegeId")
                        .HasColumnType("bigint")
                        .HasColumnName("College_Id");

                    b.Property<string>("HeadOfDepartment")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Head_Of_Department");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.HasKey("DepartmentId");

                    b.HasIndex("CollegeId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Enrollment", b =>
                {
                    b.Property<long>("EnrollmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Enrollment_Id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("EnrollmentId"));

                    b.Property<long>("CourseId")
                        .HasColumnType("bigint")
                        .HasColumnName("Course_Id");

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime")
                        .HasColumnName("Enrollment_Date");

                    b.Property<string>("Grade")
                        .HasColumnType("char(1)")
                        .HasColumnName("Grade");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint")
                        .HasColumnName("Student_Id");

                    b.HasKey("EnrollmentId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Student", b =>
                {
                    b.Property<long>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("Student_Id");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<long>("StudentId"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date")
                        .HasColumnName("Date_Of_Birth");

                    b.Property<long>("DepartmentId")
                        .HasColumnType("bigint")
                        .HasColumnName("Department_Id");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Name");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("Phone_No");

                    b.HasKey("StudentId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("CollegeApplication.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longblob");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Course", b =>
                {
                    b.HasOne("CollegeApplication.Entities.Department", "Department")
                        .WithMany("Courses")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Department", b =>
                {
                    b.HasOne("CollegeApplication.Entities.College", "College")
                        .WithMany("Departments")
                        .HasForeignKey("CollegeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("College");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Enrollment", b =>
                {
                    b.HasOne("CollegeApplication.Entities.Course", "Course")
                        .WithMany("Enrollments")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CollegeApplication.Entities.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Student", b =>
                {
                    b.HasOne("CollegeApplication.Entities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");
                });

            modelBuilder.Entity("CollegeApplication.Entities.College", b =>
                {
                    b.Navigation("Departments");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Course", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Department", b =>
                {
                    b.Navigation("Courses");
                });

            modelBuilder.Entity("CollegeApplication.Entities.Student", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}