﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RandomPic.Data;

namespace RandomPic.Migrations
{
    [DbContext(typeof(QuizContext))]
    [Migration("20190529145907_ChangeAnswerType")]
    partial class ChangeAnswerType
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("RandomPic.Model.Quiz", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Answer");

                    b.Property<bool>("HasChosen")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("Question");

                    b.Property<string>("Selections");

                    b.HasKey("Key");

                    b.ToTable("Quizzes");
                });
#pragma warning restore 612, 618
        }
    }
}