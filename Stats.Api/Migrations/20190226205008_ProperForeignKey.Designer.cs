﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stats.Api.Models;

namespace Stats.Api.Migrations
{
    [DbContext(typeof(StatsDbContext))]
    [Migration("20190226205008_ProperForeignKey")]
    partial class ProperForeignKey
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.2-servicing-10034")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Stats.Api.Models.BoxScore", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Ass");

                    b.Property<int>("BlckAg");

                    b.Property<int>("BlckFv");

                    b.Property<int>("Fg1A");

                    b.Property<int>("Fg1M");

                    b.Property<int>("Fg2A");

                    b.Property<int>("Fg2M");

                    b.Property<int>("Fg3A");

                    b.Property<int>("Fg3M");

                    b.Property<int>("FoulCm");

                    b.Property<int>("FoulRv");

                    b.Property<Guid?>("GameId");

                    b.Property<Guid?>("PlayerId");

                    b.Property<int>("RebsD");

                    b.Property<int>("RebsO");

                    b.Property<int>("SecondsPlayed");

                    b.Property<int>("St");

                    b.Property<DateTime>("Timestamp");

                    b.Property<int>("To");

                    b.Property<int>("Val");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlayerId");

                    b.ToTable("BoxScores");
                });

            modelBuilder.Entity("Stats.Api.Models.Competition", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("Stats.Api.Models.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("HomeScore");

                    b.Property<Guid?>("HomeTeamId");

                    b.Property<DateTime>("Schedule");

                    b.Property<DateTime>("Timestamp");

                    b.Property<short>("VisitorScore");

                    b.Property<Guid?>("VisitorTeamId");

                    b.HasKey("Id");

                    b.HasIndex("HomeTeamId");

                    b.HasIndex("VisitorTeamId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("Stats.Api.Models.Period", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("GameId");

                    b.Property<short>("HomeScore");

                    b.Property<DateTime>("Timestamp");

                    b.Property<short>("VisitorScore");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("Stats.Api.Models.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30);

                    b.Property<short>("Height");

                    b.Property<string>("LastName")
                        .HasMaxLength(30);

                    b.Property<int>("Position");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Stats.Api.Models.Round", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoundType");

                    b.Property<string>("SeasonId");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Rounds");
                });

            modelBuilder.Entity("Stats.Api.Models.Season", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(20);

                    b.Property<Guid>("Competition");

                    b.Property<DateTime>("Timestamp");

                    b.HasKey("Id");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("Stats.Api.Models.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(255);

                    b.Property<int?>("Country");

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<DateTime>("Timestamp");

                    b.Property<string>("Url")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Stats.Common.Dto.RoundDto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Season");

                    b.HasKey("Id");

                    b.ToTable("RoundDto");
                });

            modelBuilder.Entity("Stats.Api.Models.BoxScore", b =>
                {
                    b.HasOne("Stats.Api.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");

                    b.HasOne("Stats.Api.Models.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");
                });

            modelBuilder.Entity("Stats.Api.Models.Game", b =>
                {
                    b.HasOne("Stats.Api.Models.Team", "HomeTeam")
                        .WithMany()
                        .HasForeignKey("HomeTeamId");

                    b.HasOne("Stats.Api.Models.Team", "VisitorTeam")
                        .WithMany()
                        .HasForeignKey("VisitorTeamId");
                });

            modelBuilder.Entity("Stats.Api.Models.Period", b =>
                {
                    b.HasOne("Stats.Api.Models.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId");
                });

            modelBuilder.Entity("Stats.Api.Models.Round", b =>
                {
                    b.HasOne("Stats.Api.Models.Season", "Season")
                        .WithMany()
                        .HasForeignKey("SeasonId");
                });
#pragma warning restore 612, 618
        }
    }
}