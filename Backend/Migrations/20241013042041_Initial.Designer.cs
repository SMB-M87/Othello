﻿// <auto-generated />
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Backend.Migrations
{
    [DbContext(typeof(Database))]
    [Migration("20241013042041_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Backend.Models.Game", b =>
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
                            Board = "[[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1],[1,1,1,1,1,1,2,2],[1,1,1,1,1,1,2,2],[1,1,1,1,1,1,1,2],[1,1,1,1,1,1,1,1]]",
                            Description = "I want to player more than one game against the same adversary.",
                            FColor = 2,
                            First = " eltjo",
                            PlayersTurn = 0,
                            SColor = 1,
                            Second = "tijn",
                            Status = 2
                        });
                });

            modelBuilder.Entity("Backend.Models.GameResult", b =>
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
                            Winner = " eltjo"
                        },
                        new
                        {
                            Token = "-1",
                            Draw = "Empty",
                            Loser = " eltjo",
                            Winner = "tijn"
                        },
                        new
                        {
                            Token = "two",
                            Draw = "Empty",
                            Loser = " eltjo",
                            Winner = "tijn"
                        });
                });

            modelBuilder.Entity("Backend.Models.Player", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Friends")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PendingFriends")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Token");

                    b.ToTable("Players");

                    b.HasData(
                        new
                        {
                            Token = "karen",
                            Friends = "[]",
                            PendingFriends = "[]",
                            Username = "Karen"
                        },
                        new
                        {
                            Token = "ernst",
                            Friends = "[]",
                            PendingFriends = "[]",
                            Username = "Ersnt"
                        },
                        new
                        {
                            Token = "john",
                            Friends = "[]",
                            PendingFriends = "[]",
                            Username = "John"
                        },
                        new
                        {
                            Token = " eltjo",
                            Friends = "[]",
                            PendingFriends = "[]",
                            Username = "Eltjo"
                        },
                        new
                        {
                            Token = "tijn",
                            Friends = "[]",
                            PendingFriends = "[]",
                            Username = "Tijn"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
