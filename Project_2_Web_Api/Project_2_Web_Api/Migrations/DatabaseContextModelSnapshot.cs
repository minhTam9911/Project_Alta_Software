﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Project_2_Web_API.Models;

#nullable disable

namespace Project_2_Web_Api.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Project_2_Web_Api.Models.ApiToken", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Exipres")
                        .HasColumnType("datetime2");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("ApiTokens");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("AccountId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comment1")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int");

                    b.Property<int>("TaskId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Distributor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<Guid?>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NotificationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoAvatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("SaleManagementId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SalesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CreateBy");

                    b.HasIndex("NotificationId");

                    b.HasIndex("PositionId");

                    b.HasIndex("SaleManagementId");

                    b.HasIndex("SalesId");

                    b.ToTable("Distributors");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.GrantPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("DistributorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Module")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Permission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("StaffUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DistributorId");

                    b.HasIndex("StaffUserId");

                    b.HasIndex("UserId");

                    b.ToTable("GrantPermissions");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Media", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Medias");
                });

            modelBuilder.Entity("Project_2_Web_Api.Models.Notification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.PhotoPathAssigned", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskForVisitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskForVisitId");

                    b.ToTable("PhotoPathAssigned");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.PhotoPathReporting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskForVisitId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TaskForVisitId");

                    b.ToTable("PhotoPathReporting");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionGroupId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PositionGroupId");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.PositionGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PositionGroups");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsStatus")
                        .HasColumnType("bit");

                    b.Property<string>("PathOfTheArticle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreateBy");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.StaffUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("CreateBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fullname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsStatus")
                        .HasColumnType("bit");

                    b.Property<int?>("NotificationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoAvatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("NotificationId");

                    b.HasIndex("PositionId");

                    b.ToTable("StaffUsers");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.TaskForVisit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<Guid?>("AssignedStaffUserId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("ReportingStaffUserId")
                        .IsRequired()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VisitId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssignedStaffUserId");

                    b.HasIndex("ReportingStaffUserId");

                    b.ToTable("TaskForVisit");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<Guid?>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsStatus")
                        .HasColumnType("bit");

                    b.Property<int?>("NotificationId")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordResetToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhotoAvatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PositionId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("ResetTokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CreateBy");

                    b.HasIndex("NotificationId");

                    b.HasIndex("PositionId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Visit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Calendar")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CreateBy")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("DistributorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("GuestOfVisitId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PurposeOfVisit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TaskForVisitId")
                        .HasColumnType("int");

                    b.Property<string>("Time")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreateBy");

                    b.HasIndex("DistributorId");

                    b.HasIndex("GuestOfVisitId");

                    b.HasIndex("TaskForVisitId");

                    b.ToTable("Visits");
                });

            modelBuilder.Entity("StaffUserStaffUser", b =>
                {
                    b.Property<Guid>("StaffInteriorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StaffSuperiorId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("StaffInteriorId", "StaffSuperiorId");

                    b.HasIndex("StaffSuperiorId");

                    b.ToTable("StaffUserStaffUser");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Comment", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.TaskForVisit", "Task")
                        .WithMany("Comments")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Task");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Distributor", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.Area", "Area")
                        .WithMany("Distributors")
                        .HasForeignKey("AreaId");

                    b.HasOne("Project_2_Web_API.Models.StaffUser", "StaffUser")
                        .WithMany()
                        .HasForeignKey("CreateBy");

                    b.HasOne("Project_2_Web_Api.Models.Notification", null)
                        .WithMany("Distributors")
                        .HasForeignKey("NotificationId");

                    b.HasOne("Project_2_Web_API.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_2_Web_API.Models.StaffUser", "SaleManagement")
                        .WithMany()
                        .HasForeignKey("SaleManagementId");

                    b.HasOne("Project_2_Web_API.Models.StaffUser", "Sales")
                        .WithMany()
                        .HasForeignKey("SalesId");

                    b.Navigation("Area");

                    b.Navigation("Position");

                    b.Navigation("SaleManagement");

                    b.Navigation("Sales");

                    b.Navigation("StaffUser");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.GrantPermission", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.Distributor", null)
                        .WithMany("GrantPermissions")
                        .HasForeignKey("DistributorId");

                    b.HasOne("Project_2_Web_API.Models.StaffUser", null)
                        .WithMany("GrantPermissions")
                        .HasForeignKey("StaffUserId");

                    b.HasOne("Project_2_Web_API.Models.User", null)
                        .WithMany("GrantPermissions")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.PhotoPathAssigned", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.TaskForVisit", null)
                        .WithMany("PhotoPathAssigned")
                        .HasForeignKey("TaskForVisitId");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.PhotoPathReporting", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.TaskForVisit", null)
                        .WithMany("PhotoPathReporting")
                        .HasForeignKey("TaskForVisitId");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Position", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.PositionGroup", "PositionGroup")
                        .WithMany("Positions")
                        .HasForeignKey("PositionGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PositionGroup");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Post", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.StaffUser", "StaffUser")
                        .WithMany()
                        .HasForeignKey("CreateBy");

                    b.Navigation("StaffUser");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.StaffUser", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.Area", "Area")
                        .WithMany("StaffUsers")
                        .HasForeignKey("AreaId");

                    b.HasOne("Project_2_Web_Api.Models.Notification", null)
                        .WithMany("StaffUsers")
                        .HasForeignKey("NotificationId");

                    b.HasOne("Project_2_Web_API.Models.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.TaskForVisit", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.StaffUser", "StaffUserAssignee")
                        .WithMany()
                        .HasForeignKey("AssignedStaffUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_2_Web_API.Models.StaffUser", "StaffUserReposter")
                        .WithMany()
                        .HasForeignKey("ReportingStaffUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StaffUserAssignee");

                    b.Navigation("StaffUserReposter");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.User", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("Project_2_Web_API.Models.StaffUser", "StaffUser")
                        .WithMany()
                        .HasForeignKey("CreateBy");

                    b.HasOne("Project_2_Web_Api.Models.Notification", null)
                        .WithMany("User")
                        .HasForeignKey("NotificationId");

                    b.HasOne("Project_2_Web_API.Models.Position", "Position")
                        .WithMany("Users")
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("Position");

                    b.Navigation("StaffUser");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Visit", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.StaffUser", "StaffUser")
                        .WithMany()
                        .HasForeignKey("CreateBy");

                    b.HasOne("Project_2_Web_API.Models.Distributor", "Distributor")
                        .WithMany()
                        .HasForeignKey("DistributorId");

                    b.HasOne("Project_2_Web_API.Models.StaffUser", "GuestOfVisit")
                        .WithMany()
                        .HasForeignKey("GuestOfVisitId");

                    b.HasOne("Project_2_Web_API.Models.TaskForVisit", null)
                        .WithMany("Visits")
                        .HasForeignKey("TaskForVisitId");

                    b.Navigation("Distributor");

                    b.Navigation("GuestOfVisit");

                    b.Navigation("StaffUser");
                });

            modelBuilder.Entity("StaffUserStaffUser", b =>
                {
                    b.HasOne("Project_2_Web_API.Models.StaffUser", null)
                        .WithMany()
                        .HasForeignKey("StaffInteriorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Project_2_Web_API.Models.StaffUser", null)
                        .WithMany()
                        .HasForeignKey("StaffSuperiorId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Area", b =>
                {
                    b.Navigation("Distributors");

                    b.Navigation("StaffUsers");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Distributor", b =>
                {
                    b.Navigation("GrantPermissions");
                });

            modelBuilder.Entity("Project_2_Web_Api.Models.Notification", b =>
                {
                    b.Navigation("Distributors");

                    b.Navigation("StaffUsers");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.Position", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.PositionGroup", b =>
                {
                    b.Navigation("Positions");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.StaffUser", b =>
                {
                    b.Navigation("GrantPermissions");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.TaskForVisit", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("PhotoPathAssigned");

                    b.Navigation("PhotoPathReporting");

                    b.Navigation("Visits");
                });

            modelBuilder.Entity("Project_2_Web_API.Models.User", b =>
                {
                    b.Navigation("GrantPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
