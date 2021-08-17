﻿// <auto-generated />
using DigitalThinkers.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataAccess.Migrations
{
    [DbContext(typeof(NotesContext))]
    [Migration("20210817161641_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.9");

            modelBuilder.Entity("DigitalThinkers.DataAccess.Entities.Note", b =>
                {
                    b.Property<uint>("Denominator")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Count")
                        .HasColumnType("INTEGER");

                    b.HasKey("Denominator");

                    b.ToTable("Notes");
                });
#pragma warning restore 612, 618
        }
    }
}