﻿// <auto-generated />
using System;
using BlazorProjectApp1.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlazorProjectApp1.Data.Migrations
{
    [DbContext(typeof(ProjectDBContext))]
    [Migration("20250511163621_GuidForUserAccounts")]
    partial class GuidForUserAccounts
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("BlazorProjectApp1.Data.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("TEXT");

                    b.Property<string>("ImageFile")
                        .HasColumnType("TEXT");

                    b.Property<string>("OcrImageText")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PostId");

                    b.ToTable("Posts");

                    b.HasData(
                        new
                        {
                            PostId = 1,
                            Content = "Content for post 1",
                            Title = "Post 1",
                            Username = ""
                        },
                        new
                        {
                            PostId = 2,
                            Content = "Content for post 2",
                            Title = "Post 2",
                            Username = ""
                        },
                        new
                        {
                            PostId = 3,
                            Content = "Content for post 3",
                            Title = "Post 3",
                            Username = ""
                        },
                        new
                        {
                            PostId = 4,
                            Content = "Content for post 4",
                            Title = "Post 4",
                            Username = ""
                        },
                        new
                        {
                            PostId = 5,
                            Content = "Content for post 5",
                            Title = "Post 5",
                            Username = ""
                        },
                        new
                        {
                            PostId = 6,
                            Content = "Content for post 6",
                            Title = "Post 6",
                            Username = ""
                        });
                });

            modelBuilder.Entity("BlazorProjectApp1.Data.RawAudioData", b =>
                {
                    b.Property<int>("PostId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AudioId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("AudioBinaryData")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("PostId", "AudioId");

                    b.HasIndex("PostId")
                        .IsUnique();

                    b.ToTable("RawAudioData");
                });

            modelBuilder.Entity("BlazorProjectApp1.Entities.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasColumnName("Id");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT")
                        .HasColumnName("Username");

                    b.HasKey("Id");

                    b.ToTable("UserAccount");
                });

            modelBuilder.Entity("BlazorProjectApp1.Data.RawAudioData", b =>
                {
                    b.HasOne("BlazorProjectApp1.Data.Post", "RelevantPost")
                        .WithOne("AudioData")
                        .HasForeignKey("BlazorProjectApp1.Data.RawAudioData", "PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RelevantPost");
                });

            modelBuilder.Entity("BlazorProjectApp1.Data.Post", b =>
                {
                    b.Navigation("AudioData");
                });
#pragma warning restore 612, 618
        }
    }
}
