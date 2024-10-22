﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SessionAssistant.API.Persistence;

#nullable disable

namespace SessionAssistant.API.Persistence.Migrations
{
    [DbContext(typeof(SessionAssistantReadDbContext))]
    partial class SessionAssistantDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("SessionAssistant.API.Persistence.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanDodge")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CanParry")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Characters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CanDodge = true,
                            CanParry = true,
                            Name = "Gerlach Bauer"
                        },
                        new
                        {
                            Id = 2,
                            CanDodge = false,
                            CanParry = true,
                            Name = "Roborbor"
                        },
                        new
                        {
                            Id = 3,
                            CanDodge = false,
                            CanParry = false,
                            Name = "Pan Robak"
                        },
                        new
                        {
                            Id = 4,
                            CanDodge = true,
                            CanParry = true,
                            Name = "Chad Poggington"
                        });
                });

            modelBuilder.Entity("SessionAssistant.Shared.DTOs.Combat.EncounterDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentRound")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Encounters");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentRound = 1
                        },
                        new
                        {
                            Id = 2,
                            CurrentRound = 1
                        });
                });

            modelBuilder.Entity("SessionAssistant.Shared.DTOs.Combat.EncounterDTO", b =>
                {
                    b.OwnsMany("SessionAssistant.Shared.DTOs.Combat.CombatantDTO", "Combatants", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Attacks")
                                .HasColumnType("INTEGER");

                            b1.Property<bool>("CanAct")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("EncounterDTOId")
                                .HasColumnType("INTEGER");

                            b1.Property<int>("Initiative")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasColumnType("TEXT");

                            b1.HasKey("Id");

                            b1.HasIndex("EncounterDTOId");

                            b1.ToTable("Combatants", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("EncounterDTOId");

                            b1.OwnsMany("SessionAssistant.Shared.DTOs.Combat.SkillDTO", "Skills", b2 =>
                                {
                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("CombatantDTOId")
                                        .HasColumnType("INTEGER");

                                    b2.Property<int>("Cooldown")
                                        .HasColumnType("INTEGER");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Icon")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasColumnType("TEXT");

                                    b2.HasKey("Id");

                                    b2.HasIndex("CombatantDTOId");

                                    b2.ToTable("Skills", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("CombatantDTOId");
                                });

                            b1.Navigation("Skills");
                        });

                    b.Navigation("Combatants");
                });
#pragma warning restore 612, 618
        }
    }
}
