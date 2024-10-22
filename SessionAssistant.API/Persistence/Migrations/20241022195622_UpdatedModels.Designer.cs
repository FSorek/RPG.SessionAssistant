﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SessionAssistant.API.Persistence;

#nullable disable

namespace SessionAssistant.API.Persistence.Migrations
{
    [DbContext(typeof(SessionAssistantReadDbContext))]
    [Migration("20241022195622_UpdatedModels")]
    partial class UpdatedModels
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("CombatantDTOSkillDTO", b =>
                {
                    b.Property<int>("CombatantsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SkillsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CombatantsId", "SkillsId");

                    b.HasIndex("SkillsId");

                    b.ToTable("CombatantDTOSkillDTO");
                });

            modelBuilder.Entity("SessionAssistant.Shared.DTOs.Combat.CombatantDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActPriority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Attacks")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EncounterId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasCompletedRound")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Initiative")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EncounterId");

                    b.ToTable("Combatants", (string)null);
                });

            modelBuilder.Entity("SessionAssistant.Shared.DTOs.Combat.EncounterDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActingInitiative")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActingPriority")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CurrentRound")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Encounters", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActingInitiative = 100,
                            ActingPriority = 0,
                            CurrentRound = 1
                        },
                        new
                        {
                            Id = 2,
                            ActingInitiative = 100,
                            ActingPriority = 0,
                            CurrentRound = 1
                        });
                });

            modelBuilder.Entity("SessionAssistant.Shared.DTOs.Combat.SkillDTO", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Cooldown")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Skills", (string)null);
                });

            modelBuilder.Entity("CombatantDTOSkillDTO", b =>
                {
                    b.HasOne("SessionAssistant.Shared.DTOs.Combat.CombatantDTO", null)
                        .WithMany()
                        .HasForeignKey("CombatantsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SessionAssistant.Shared.DTOs.Combat.SkillDTO", null)
                        .WithMany()
                        .HasForeignKey("SkillsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SessionAssistant.Shared.DTOs.Combat.CombatantDTO", b =>
                {
                    b.HasOne("SessionAssistant.Shared.DTOs.Combat.EncounterDTO", null)
                        .WithMany("Combatants")
                        .HasForeignKey("EncounterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SessionAssistant.Shared.DTOs.Combat.EncounterDTO", b =>
                {
                    b.Navigation("Combatants");
                });
#pragma warning restore 612, 618
        }
    }
}
