﻿// <auto-generated />
using System;
using DoorManagementSystem.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DoorManagementSystem.Infrastructure.Migrations
{
    [DbContext(typeof(DoorManagementContext))]
    [Migration("20240227181624_TestingRound")]
    partial class TestingRound
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("door_user")
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.DoorLogs", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("log_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LogId"));

                    b.Property<DateTime>("AccessDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("access_date_time");

                    b.Property<int>("DoorID")
                        .HasColumnType("integer")
                        .HasColumnName("door_id");

                    b.Property<bool>("IsRemoteAccessRequested")
                        .HasColumnType("boolean")
                        .HasColumnName("is_remote_access_requested");

                    b.Property<bool>("Success")
                        .HasColumnType("boolean")
                        .HasColumnName("success");

                    b.Property<int>("UserID")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("LogId");

                    b.HasIndex("AccessDateTime");

                    b.HasIndex("DoorID");

                    b.HasIndex("UserID");

                    b.ToTable("door_logs", "door_user");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.Doors", b =>
                {
                    b.Property<int>("DoorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("door_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("DoorId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("DoorName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("door_name");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("location");

                    b.Property<bool>("RemoteAccessEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("remote_acccess_enabled");

                    b.HasKey("DoorId");

                    b.HasIndex("Location");

                    b.ToTable("doors", "door_user");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.RoleDoorAccess", b =>
                {
                    b.Property<int>("RoleDoorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("role_door_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleDoorId"));

                    b.Property<int>("DoorId")
                        .HasColumnType("integer")
                        .HasColumnName("door_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("RoleDoorId");

                    b.HasIndex("DoorId");

                    b.HasIndex("RoleId");

                    b.ToTable("role_door", "door_user");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.Roles", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role_name");

                    b.HasKey("RoleId");

                    b.ToTable("roles", "door_user");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.UserRoles", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_role_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserRoleId"));

                    b.Property<bool>("AdminRole")
                        .HasColumnType("boolean")
                        .HasColumnName("admin_role");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("UserRoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("user_roles", "door_user");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.UserTags", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("tag_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TagId"));

                    b.Property<string>("TagCode")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)")
                        .HasColumnName("tag_code");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("TagId");

                    b.HasIndex("UserId");

                    b.ToTable("user_tags", "door_user");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("last_name");

                    b.Property<string>("PinHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("pin_hash");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", "door_user");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.DoorLogs", b =>
                {
                    b.HasOne("DoorManagementSystem.Domain.Entities.Doors", "Door")
                        .WithMany("DoorLogs")
                        .HasForeignKey("DoorID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoorManagementSystem.Domain.Entities.Users", "User")
                        .WithMany("DoorLogs")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Door");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.RoleDoorAccess", b =>
                {
                    b.HasOne("DoorManagementSystem.Domain.Entities.Doors", "Door")
                        .WithMany("RoleDoors")
                        .HasForeignKey("DoorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoorManagementSystem.Domain.Entities.Roles", "Role")
                        .WithMany("RoleDoors")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Door");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.UserRoles", b =>
                {
                    b.HasOne("DoorManagementSystem.Domain.Entities.Roles", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DoorManagementSystem.Domain.Entities.Users", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.UserTags", b =>
                {
                    b.HasOne("DoorManagementSystem.Domain.Entities.Users", "User")
                        .WithMany("UserTags")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.Doors", b =>
                {
                    b.Navigation("DoorLogs");

                    b.Navigation("RoleDoors");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.Roles", b =>
                {
                    b.Navigation("RoleDoors");

                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("DoorManagementSystem.Domain.Entities.Users", b =>
                {
                    b.Navigation("DoorLogs");

                    b.Navigation("UserRoles");

                    b.Navigation("UserTags");
                });
#pragma warning restore 612, 618
        }
    }
}