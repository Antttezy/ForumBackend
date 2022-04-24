﻿// <auto-generated />
using ForumBackend.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ForumBackendApi.Migrations
{
    [DbContext(typeof(ForumContext))]
    [Migration("20220424111417_Comments")]
    partial class Comments
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ForumBackend.Core.Model.ForumComment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorRef")
                        .HasColumnType("integer");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("bigint");

                    b.Property<int>("PostRef")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorRef");

                    b.HasIndex("PostRef");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("ForumBackend.Core.Model.ForumPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AuthorRef")
                        .HasColumnType("integer");

                    b.Property<long>("CreatedAt")
                        .HasColumnType("bigint");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorRef");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("ForumBackend.Core.Model.ForumUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasMaxLength(127)
                        .HasColumnType("character varying(127)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ForumBackend.Core.Model.UserAuth", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserRef")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserRef")
                        .IsUnique();

                    b.ToTable("Authentication");
                });

            modelBuilder.Entity("ForumBackend.Core.Model.ForumComment", b =>
                {
                    b.HasOne("ForumBackend.Core.Model.ForumUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ForumBackend.Core.Model.ForumPost", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("ForumBackend.Core.Model.ForumPost", b =>
                {
                    b.HasOne("ForumBackend.Core.Model.ForumUser", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("ForumBackend.Core.Model.UserAuth", b =>
                {
                    b.HasOne("ForumBackend.Core.Model.ForumUser", "User")
                        .WithOne("UserAuth")
                        .HasForeignKey("ForumBackend.Core.Model.UserAuth", "UserRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ForumBackend.Core.Model.ForumPost", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("ForumBackend.Core.Model.ForumUser", b =>
                {
                    b.Navigation("UserAuth")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
