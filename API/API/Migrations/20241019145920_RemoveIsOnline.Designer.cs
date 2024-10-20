﻿// <auto-generated />
using System;
using API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20241019145920_RemoveIsOnline")]
    partial class RemoveIsOnline
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Game", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Board")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FColor")
                        .HasColumnType("int");

                    b.Property<string>("First")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayersTurn")
                        .HasColumnType("int");

                    b.Property<int>("SColor")
                        .HasColumnType("int");

                    b.Property<string>("Second")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Token");

                    b.ToTable("Games");

                    b.HasData(
                        new
                        {
                            Token = "zero",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Description = "I wanna play a game and don't have any requirements.",
                            FColor = 2,
                            First = "karen",
                            PlayersTurn = 2,
                            SColor = 1,
                            Second = "",
                            Status = 0
                        },
                        new
                        {
                            Token = "one",
                            Board = "[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Description = "I search an advanced player!",
                            FColor = 2,
                            First = "ernst",
                            PlayersTurn = 2,
                            SColor = 1,
                            Second = "john",
                            Status = 1
                        },
                        new
                        {
                            Token = "two",
                            Board = "[[2,1,1,0,0,0,0,0],[2,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]]",
                            Description = "I want to player more than one game against the same adversary.",
                            FColor = 2,
                            First = "eltjo",
                            PlayersTurn = 1,
                            SColor = 1,
                            Second = "tijn",
                            Status = 1
                        });
                });

            modelBuilder.Entity("API.Models.GameResult", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Draw")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Loser")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Winner")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Token");

                    b.ToTable("Results");

                    b.HasData(
                        new
                        {
                            Token = "-2",
                            Draw = "Empty",
                            Loser = "tijn",
                            Winner = "eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Draw = "Empty",
                            Loser = "eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "-0",
                            Draw = "Empty",
                            Loser = "eltjo",
                            Winner = "tijn"
                        });
                });

            modelBuilder.Entity("API.Models.Player", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Friends")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastActivity")
                        .HasColumnType("datetime2");

                    b.Property<string>("PendingFriends")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Username");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Username = "Karen",
                            Friends = "[\"Ernst\",\"John\"]",
                            LastActivity = new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6521),
                            PendingFriends = "[]",
                            Token = "karen"
                        },
                        new
                        {
                            Username = "Ernst",
                            Friends = "[\"John\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6528),
                            PendingFriends = "[]",
                            Token = "ernst"
                        },
                        new
                        {
                            Username = "John",
                            Friends = "[\"Ernst\",\"Karen\"]",
                            LastActivity = new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6530),
                            PendingFriends = "[]",
                            Token = "john"
                        },
                        new
                        {
                            Username = "Eltjo",
                            Friends = "[\"Tijn\"]",
                            LastActivity = new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6532),
                            PendingFriends = "[\"Karen\",\"Ernst\",\"John\"]",
                            Token = "eltjo"
                        },
                        new
                        {
                            Username = "Tijn",
                            Friends = "[\"Eltjo\"]",
                            LastActivity = new DateTime(2024, 10, 19, 14, 59, 20, 232, DateTimeKind.Utc).AddTicks(6534),
                            PendingFriends = "[\"Karen\",\"Ernst\",\"John\"]",
                            Token = "tijn"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
