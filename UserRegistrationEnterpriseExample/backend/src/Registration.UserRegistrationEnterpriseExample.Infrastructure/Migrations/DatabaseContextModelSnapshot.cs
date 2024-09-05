﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Registration.UserRegistrationEnterpriseExample.Infrastructure.PostgreSql;

#nullable disable

namespace Registration.UserRegistrationEnterpriseExample.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Registration.UserRegistrationEnterpriseExample.Domain.Entidades.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("criado_em");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("Document")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("document");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("ultima_modificacao_em");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("OriginTimestampUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("origin_timestamp_utc");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<int>("UserType")
                        .HasColumnType("integer")
                        .HasColumnName("user_type");

                    b.HasKey("Id");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Registration.UserRegistrationEnterpriseExample.Domain.Entidades.UserWorkshop", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WorkshopId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("criado_em");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("ultima_modificacao_em");

                    b.Property<DateTime?>("OriginTimestampUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("origin_timestamp_utc");

                    b.HasKey("UserId", "WorkshopId");

                    b.HasIndex("WorkshopId");

                    b.ToTable("user_workshops", (string)null);
                });

            modelBuilder.Entity("Registration.UserRegistrationEnterpriseExample.Domain.Entidades.WorkShop", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Address");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("criado_em");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<bool>("Deleted")
                        .HasColumnType("boolean")
                        .HasColumnName("deleted");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<string>("IdCreator")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("id_creator");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image");

                    b.Property<string>("ImageCreator")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("image_creator");

                    b.Property<DateTime>("LastModifiedAt")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("ultima_modificacao_em");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("OriginTimestampUtc")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("origin_timestamp_utc");

                    b.HasKey("Id");

                    b.ToTable("workshops", (string)null);
                });

            modelBuilder.Entity("Registration.UserRegistrationEnterpriseExample.Domain.Entidades.UserWorkshop", b =>
                {
                    b.HasOne("Registration.UserRegistrationEnterpriseExample.Domain.Entidades.User", "User")
                        .WithMany("WorkShopsSubscribed")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Registration.UserRegistrationEnterpriseExample.Domain.Entidades.WorkShop", "WorkShop")
                        .WithMany("UsersSubscribed")
                        .HasForeignKey("WorkshopId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WorkShop");
                });

            modelBuilder.Entity("Registration.UserRegistrationEnterpriseExample.Domain.Entidades.User", b =>
                {
                    b.Navigation("WorkShopsSubscribed");
                });

            modelBuilder.Entity("Registration.UserRegistrationEnterpriseExample.Domain.Entidades.WorkShop", b =>
                {
                    b.Navigation("UsersSubscribed");
                });
#pragma warning restore 612, 618
        }
    }
}
