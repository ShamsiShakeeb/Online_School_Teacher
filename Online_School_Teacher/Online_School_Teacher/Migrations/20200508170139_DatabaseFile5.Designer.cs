﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Online_School_Teacher.Models;

namespace Online_School_Teacher.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20200508170139_DatabaseFile5")]
    partial class DatabaseFile5
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Online_School_Teacher.Models.Course", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TeacherEmail")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("TeacherEmail");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.Student", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Email");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.StudentBasicInformation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("StudentEmail")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("StudentEmail");

                    b.ToTable("Student_Basic_Information");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.Teacher", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Approve")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Email");

                    b.ToTable("Teacher");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.TeacherBasicInformation", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("TeacherEmail")
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("TeacherEmail");

                    b.ToTable("Teacher_Basic_Information");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.Tutorial", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Approve")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int?>("CourseID")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("VideoFileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ID");

                    b.HasIndex("CourseID");

                    b.ToTable("Tutorial");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.Course", b =>
                {
                    b.HasOne("Online_School_Teacher.Models.Teacher", null)
                        .WithMany("course")
                        .HasForeignKey("TeacherEmail");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.StudentBasicInformation", b =>
                {
                    b.HasOne("Online_School_Teacher.Models.Student", null)
                        .WithMany("Student_Basic_Info")
                        .HasForeignKey("StudentEmail");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.TeacherBasicInformation", b =>
                {
                    b.HasOne("Online_School_Teacher.Models.Teacher", null)
                        .WithMany("Teacher_Basic_Information")
                        .HasForeignKey("TeacherEmail");
                });

            modelBuilder.Entity("Online_School_Teacher.Models.Tutorial", b =>
                {
                    b.HasOne("Online_School_Teacher.Models.Course", null)
                        .WithMany("tutorial")
                        .HasForeignKey("CourseID");
                });
#pragma warning restore 612, 618
        }
    }
}
