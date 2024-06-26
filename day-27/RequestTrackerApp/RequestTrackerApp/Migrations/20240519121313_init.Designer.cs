﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RequestTrackerApp.Context;

#nullable disable

namespace RequestTrackerApp.Migrations
{
    [DbContext(typeof(RequestTrackercontext))]
    [Migration("20240519121313_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RequestTrackerApp.Model.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("HasCode")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Password")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.RequestSolution", b =>
                {
                    b.Property<int>("SolutionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SolutionId"));

                    b.Property<bool>("IsSolved")
                        .HasColumnType("bit");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.Property<string>("RequestRaiserComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SolutionDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SolvedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("SolvedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("SolutionId");

                    b.HasIndex("RequestId");

                    b.HasIndex("SolvedBy");

                    b.ToTable("RequestSolution");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.Requests", b =>
                {
                    b.Property<int>("RequestNumber")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestNumber"));

                    b.Property<DateTime?>("ClosedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("RequestClosedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestRaisedBy")
                        .HasColumnType("int");

                    b.Property<string>("RequestStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RequestNumber");

                    b.HasIndex("RequestClosedBy");

                    b.HasIndex("RequestRaisedBy");

                    b.ToTable("Requests");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.SolutionFeedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<int>("FeedbackBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("FeedbackDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Rating")
                        .HasColumnType("real");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RequestId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("FeedbackBy");

                    b.HasIndex("RequestId");

                    b.ToTable("SolutionFeedback");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.SolutionResposnse", b =>
                {
                    b.Property<int>("ResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ResponseId"));

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Response")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SolutionId")
                        .HasColumnType("int");

                    b.HasKey("ResponseId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("SolutionId")
                        .IsUnique();

                    b.ToTable("SolutionResposnse");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.RequestSolution", b =>
                {
                    b.HasOne("RequestTrackerApp.Model.Requests", "RequestRaised")
                        .WithMany("SolutionsProvided")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RequestTrackerApp.Model.Employee", "SolvedByEmployee")
                        .WithMany("SolutionsProvided")
                        .HasForeignKey("SolvedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RequestRaised");

                    b.Navigation("SolvedByEmployee");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.Requests", b =>
                {
                    b.HasOne("RequestTrackerApp.Model.Employee", "RequestClosedByEmployee")
                        .WithMany("RequestsClosed")
                        .HasForeignKey("RequestClosedBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("RequestTrackerApp.Model.Employee", "RaisedByEmployee")
                        .WithMany("RequestsRaised")
                        .HasForeignKey("RequestRaisedBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("RaisedByEmployee");

                    b.Navigation("RequestClosedByEmployee");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.SolutionFeedback", b =>
                {
                    b.HasOne("RequestTrackerApp.Model.Employee", "FeedbackByEmployee")
                        .WithMany("FeedbacksGiven")
                        .HasForeignKey("FeedbackBy")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RequestTrackerApp.Model.Requests", "Request")
                        .WithMany("FeedbackProvided")
                        .HasForeignKey("RequestId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FeedbackByEmployee");

                    b.Navigation("Request");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.SolutionResposnse", b =>
                {
                    b.HasOne("RequestTrackerApp.Model.Employee", "Employee")
                        .WithMany("ResponseGiven")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("RequestTrackerApp.Model.RequestSolution", "RequestSolution")
                        .WithOne("Response")
                        .HasForeignKey("RequestTrackerApp.Model.SolutionResposnse", "SolutionId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Employee");

                    b.Navigation("RequestSolution");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.Employee", b =>
                {
                    b.Navigation("FeedbacksGiven");

                    b.Navigation("RequestsClosed");

                    b.Navigation("RequestsRaised");

                    b.Navigation("ResponseGiven");

                    b.Navigation("SolutionsProvided");
                });

            modelBuilder.Entity("RequestTrackerApp.Model.RequestSolution", b =>
                {
                    b.Navigation("Response")
                        .IsRequired();
                });

            modelBuilder.Entity("RequestTrackerApp.Model.Requests", b =>
                {
                    b.Navigation("FeedbackProvided");

                    b.Navigation("SolutionsProvided");
                });
#pragma warning restore 612, 618
        }
    }
}
