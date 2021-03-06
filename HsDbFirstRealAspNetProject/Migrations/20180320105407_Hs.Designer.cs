﻿// <auto-generated />
using HsDbFirstRealAspNetProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace HsDbFirstRealAspNetProject.Migrations
{
    [DbContext(typeof(HsDbContext))]
    [Migration("20180320105407_Hs")]
    partial class Hs
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Hearthstone.Mechanic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardInfoId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("CardInfoId");

                    b.ToTable("Mechanic");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.AdditionCardInfo", b =>
                {
                    b.Property<int>("AdditionCardInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Artist");

                    b.Property<bool>("Collectible");

                    b.Property<long>("Cost");

                    b.Property<string>("Rarity");

                    b.HasKey("AdditionCardInfoId");

                    b.ToTable("additionalCardInfo");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.CardInfo", b =>
                {
                    b.Property<int>("CardInfoId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdditionCardInfoId");

                    b.Property<string>("CardId");

                    b.Property<string>("CardSet");

                    b.Property<string>("Class");

                    b.Property<string>("DbId");

                    b.Property<string>("Img");

                    b.Property<string>("Name");

                    b.Property<string>("Text");

                    b.Property<string>("Type");

                    b.HasKey("CardInfoId");

                    b.HasIndex("AdditionCardInfoId");

                    b.ToTable("CardInfo");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.Deck", b =>
                {
                    b.Property<int>("DeckId")
                        .ValueGeneratedOnAdd();

                    b.HasKey("DeckId");

                    b.ToTable("Deck");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.DeckVsCards", b =>
                {
                    b.Property<int>("DeckVsCardsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardInfoId");

                    b.Property<int?>("DeckId");

                    b.HasKey("DeckVsCardsId");

                    b.HasIndex("CardInfoId");

                    b.HasIndex("DeckId");

                    b.ToTable("DeckVsCards");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.Hero", b =>
                {
                    b.Property<int>("HeroId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardInfoAdditionCardInfoId");

                    b.Property<long>("Health");

                    b.HasKey("HeroId");

                    b.HasIndex("CardInfoAdditionCardInfoId");

                    b.ToTable("Hero");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.Minion", b =>
                {
                    b.Property<int>("MinionId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("Attack");

                    b.Property<int?>("CardInfoAdditionCardInfoId");

                    b.Property<long>("Health");

                    b.HasKey("MinionId");

                    b.HasIndex("CardInfoAdditionCardInfoId");

                    b.ToTable("Minion");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.MinionsVsMechanic", b =>
                {
                    b.Property<int>("MinionsVsMechanicId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("MechanicId");

                    b.Property<int?>("MinionId");

                    b.HasKey("MinionsVsMechanicId");

                    b.HasIndex("MechanicId");

                    b.HasIndex("MinionId");

                    b.ToTable("MinionsVsMechanics");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.Spell", b =>
                {
                    b.Property<int>("SpellId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CardInfoAdditionCardInfoId");

                    b.Property<string>("Flavor");

                    b.Property<string>("HowToGet");

                    b.HasKey("SpellId");

                    b.HasIndex("CardInfoAdditionCardInfoId");

                    b.ToTable("Spell");
                });

            modelBuilder.Entity("Hearthstone.Mechanic", b =>
                {
                    b.HasOne("HsDbFirstRealAspNetProject.Models.DbModel.CardInfo")
                        .WithMany("Mechanic")
                        .HasForeignKey("CardInfoId");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.CardInfo", b =>
                {
                    b.HasOne("HsDbFirstRealAspNetProject.Models.DbModel.AdditionCardInfo", "AdditionCard")
                        .WithMany()
                        .HasForeignKey("AdditionCardInfoId");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.DeckVsCards", b =>
                {
                    b.HasOne("HsDbFirstRealAspNetProject.Models.DbModel.CardInfo", "Card")
                        .WithMany()
                        .HasForeignKey("CardInfoId");

                    b.HasOne("HsDbFirstRealAspNetProject.Models.DbModel.Deck", "Deck")
                        .WithMany()
                        .HasForeignKey("DeckId");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.Hero", b =>
                {
                    b.HasOne("HsDbFirstRealAspNetProject.Models.DbModel.AdditionCardInfo", "CardInfo")
                        .WithMany()
                        .HasForeignKey("CardInfoAdditionCardInfoId");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.Minion", b =>
                {
                    b.HasOne("HsDbFirstRealAspNetProject.Models.DbModel.AdditionCardInfo", "CardInfo")
                        .WithMany()
                        .HasForeignKey("CardInfoAdditionCardInfoId");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.MinionsVsMechanic", b =>
                {
                    b.HasOne("Hearthstone.Mechanic", "Mechanic")
                        .WithMany()
                        .HasForeignKey("MechanicId");

                    b.HasOne("HsDbFirstRealAspNetProject.Models.DbModel.Minion", "Minion")
                        .WithMany()
                        .HasForeignKey("MinionId");
                });

            modelBuilder.Entity("HsDbFirstRealAspNetProject.Models.DbModel.Spell", b =>
                {
                    b.HasOne("HsDbFirstRealAspNetProject.Models.DbModel.AdditionCardInfo", "CardInfo")
                        .WithMany()
                        .HasForeignKey("CardInfoAdditionCardInfoId");
                });
#pragma warning restore 612, 618
        }
    }
}
