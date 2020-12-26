﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Rpg_Restapi.Data;

namespace Rpg_Restapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201226173555_Seeding")]
    partial class Seeding
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Rpg_Restapi.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Class")
                        .HasColumnType("integer");

                    b.Property<int>("Defeats")
                        .HasColumnType("integer");

                    b.Property<int>("Defense")
                        .HasColumnType("integer");

                    b.Property<int>("Fights")
                        .HasColumnType("integer");

                    b.Property<int>("HitPoints")
                        .HasColumnType("integer");

                    b.Property<int>("Intelligence")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Strength")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("Victories")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Class = 1,
                            Defeats = 0,
                            Defense = 10,
                            Fights = 0,
                            HitPoints = 100,
                            Intelligence = 10,
                            Name = "Frodo",
                            Strength = 15,
                            UserId = 1,
                            Victories = 0
                        },
                        new
                        {
                            Id = 2,
                            Class = 2,
                            Defeats = 0,
                            Defense = 5,
                            Fights = 0,
                            HitPoints = 100,
                            Intelligence = 20,
                            Name = "Raistlin",
                            Strength = 5,
                            UserId = 2,
                            Victories = 0
                        });
                });

            modelBuilder.Entity("Rpg_Restapi.Models.CharacterSkill", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<int>("SkillId")
                        .HasColumnType("integer");

                    b.HasKey("CharacterId", "SkillId");

                    b.HasIndex("SkillId");

                    b.ToTable("CharacterSkills");

                    b.HasData(
                        new
                        {
                            CharacterId = 1,
                            SkillId = 2
                        },
                        new
                        {
                            CharacterId = 2,
                            SkillId = 1
                        },
                        new
                        {
                            CharacterId = 2,
                            SkillId = 3
                        });
                });

            modelBuilder.Entity("Rpg_Restapi.Models.Skill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Damage = 30,
                            Name = "Fireball"
                        },
                        new
                        {
                            Id = 2,
                            Damage = 20,
                            Name = "Frenzy"
                        },
                        new
                        {
                            Id = 3,
                            Damage = 50,
                            Name = "Blizzard"
                        });
                });

            modelBuilder.Entity("Rpg_Restapi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("bytea");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("bytea");

                    b.Property<string>("Role")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Player");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            PasswordHash = new byte[] { 82, 208, 159, 140, 109, 73, 41, 127, 23, 55, 206, 4, 151, 14, 182, 66, 39, 56, 93, 6, 124, 153, 247, 137, 117, 17, 57, 237, 94, 253, 4, 65, 158, 9, 28, 254, 147, 52, 116, 109, 169, 231, 14, 55, 11, 126, 220, 184, 64, 247, 31, 231, 120, 187, 182, 131, 129, 10, 63, 4, 134, 169, 91, 12 },
                            PasswordSalt = new byte[] { 228, 70, 203, 61, 64, 148, 221, 31, 69, 161, 75, 234, 178, 93, 20, 159, 118, 222, 192, 249, 33, 131, 66, 202, 161, 197, 131, 136, 167, 97, 252, 60, 70, 143, 63, 124, 235, 79, 174, 38, 232, 34, 75, 40, 171, 20, 124, 249, 8, 77, 39, 20, 15, 211, 93, 226, 124, 44, 121, 205, 154, 130, 135, 140, 172, 168, 35, 151, 174, 130, 134, 246, 41, 40, 107, 167, 130, 225, 30, 152, 167, 94, 132, 118, 251, 72, 56, 43, 60, 94, 7, 15, 100, 57, 178, 24, 233, 45, 11, 138, 74, 188, 66, 214, 72, 102, 171, 81, 107, 141, 186, 40, 62, 7, 102, 59, 56, 88, 163, 152, 33, 186, 225, 62, 202, 6, 10, 52 },
                            Role = "Player",
                            Username = "user1"
                        },
                        new
                        {
                            Id = 2,
                            PasswordHash = new byte[] { 82, 208, 159, 140, 109, 73, 41, 127, 23, 55, 206, 4, 151, 14, 182, 66, 39, 56, 93, 6, 124, 153, 247, 137, 117, 17, 57, 237, 94, 253, 4, 65, 158, 9, 28, 254, 147, 52, 116, 109, 169, 231, 14, 55, 11, 126, 220, 184, 64, 247, 31, 231, 120, 187, 182, 131, 129, 10, 63, 4, 134, 169, 91, 12 },
                            PasswordSalt = new byte[] { 228, 70, 203, 61, 64, 148, 221, 31, 69, 161, 75, 234, 178, 93, 20, 159, 118, 222, 192, 249, 33, 131, 66, 202, 161, 197, 131, 136, 167, 97, 252, 60, 70, 143, 63, 124, 235, 79, 174, 38, 232, 34, 75, 40, 171, 20, 124, 249, 8, 77, 39, 20, 15, 211, 93, 226, 124, 44, 121, 205, 154, 130, 135, 140, 172, 168, 35, 151, 174, 130, 134, 246, 41, 40, 107, 167, 130, 225, 30, 152, 167, 94, 132, 118, 251, 72, 56, 43, 60, 94, 7, 15, 100, 57, 178, 24, 233, 45, 11, 138, 74, 188, 66, 214, 72, 102, 171, 81, 107, 141, 186, 40, 62, 7, 102, 59, 56, 88, 163, 152, 33, 186, 225, 62, 202, 6, 10, 52 },
                            Role = "Player",
                            Username = "user2"
                        });
                });

            modelBuilder.Entity("Rpg_Restapi.Models.Weapon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<int>("Damage")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Weapons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CharacterId = 1,
                            Damage = 20,
                            Name = "The Master Sword"
                        },
                        new
                        {
                            Id = 2,
                            CharacterId = 2,
                            Damage = 5,
                            Name = "Crystal Wand"
                        });
                });

            modelBuilder.Entity("Rpg_Restapi.Models.Character", b =>
                {
                    b.HasOne("Rpg_Restapi.Models.User", "User")
                        .WithMany("Characters")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Rpg_Restapi.Models.CharacterSkill", b =>
                {
                    b.HasOne("Rpg_Restapi.Models.Character", "Character")
                        .WithMany("CharacterSkills")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rpg_Restapi.Models.Skill", "Skill")
                        .WithMany("CharacterSkills")
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Rpg_Restapi.Models.Weapon", b =>
                {
                    b.HasOne("Rpg_Restapi.Models.Character", "Character")
                        .WithOne("Weapon")
                        .HasForeignKey("Rpg_Restapi.Models.Weapon", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
