﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ParcelTracker.Infrastructure.Contexts;

#nullable disable

namespace ParcelTracker.Infrastructure.Migrations
{
    [DbContext(typeof(NotificationsContext))]
    [Migration("20240516201126_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.30");

            modelBuilder.Entity("ParcelTracker.Infrastructure.Entities.NotificationEntity", b =>
                {
                    b.Property<long>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("TEXT");

                    b.Property<int>("NotificationType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReferenceId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("NotificationId");

                    b.ToTable("Notifications", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
