﻿// <auto-generated />
using System;
using GCP.RazorPagesApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GCP.RazorPagesApp.Data.Migrations
{
    [DbContext(typeof(GCPContext))]
    partial class GCPContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "link_type", new[] { "unknown", "article", "store", "image", "video" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("MaxPlayers")
                        .HasColumnType("integer")
                        .HasColumnName("max_players");

                    b.Property<string>("NormalizedTitle")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("normalized_title");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("date")
                        .HasColumnName("release_date");

                    b.Property<bool>("SupportsOnlinePlay")
                        .HasColumnType("boolean")
                        .HasColumnName("supports_online_play");

                    b.Property<string>("Title")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_game");

                    b.HasIndex("NormalizedTitle")
                        .IsUnique()
                        .HasDatabaseName("ix_game_normalized_title")
                        .HasAnnotation("Npgsql:CreatedConcurrently", true);

                    b.ToTable("game", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GameEndorsement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<double>("Weight")
                        .HasColumnType("double precision")
                        .HasColumnName("weight");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_game_endorsement");

                    b.HasIndex("GameId")
                        .HasDatabaseName("ix_game_endorsement_game_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_game_endorsement_user_id");

                    b.ToTable("game_endorsement", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GameGenre", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer")
                        .HasColumnName("genre_id");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("GameId", "GenreId")
                        .HasName("pk_game_genre");

                    b.HasIndex("GenreId")
                        .HasDatabaseName("ix_game_genre_genre_id");

                    b.ToTable("game_genre", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GameLink", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<string>("Label")
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("label");

                    b.Property<string>("Link")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("link");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("type");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_game_link");

                    b.HasIndex("GameId")
                        .HasDatabaseName("ix_game_link_game_id");

                    b.HasIndex("Link")
                        .IsUnique()
                        .HasDatabaseName("ix_game_link_link")
                        .HasAnnotation("Npgsql:CreatedConcurrently", true);

                    b.ToTable("game_link", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GamePublisher", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer")
                        .HasColumnName("publisher_id");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("GameId", "PublisherId")
                        .HasName("pk_game_publisher");

                    b.HasIndex("PublisherId")
                        .HasDatabaseName("ix_game_publisher_publisher_id");

                    b.ToTable("game_publisher", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GamePublisherPlatform", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("PublisherId")
                        .HasColumnType("integer")
                        .HasColumnName("publisher_id");

                    b.Property<int>("PlatformId")
                        .HasColumnType("integer")
                        .HasColumnName("platform_id");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("GameId", "PublisherId", "PlatformId")
                        .HasName("pk_game_publisher_platform");

                    b.HasIndex("PlatformId")
                        .HasDatabaseName("ix_game_publisher_platform_platform_id");

                    b.ToTable("game_publisher_platform", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GameRetailer", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("RetailerId")
                        .HasColumnType("integer")
                        .HasColumnName("retailer_id");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("GameId", "RetailerId")
                        .HasName("pk_game_retailer");

                    b.HasIndex("RetailerId")
                        .HasDatabaseName("ix_game_retailer_retailer_id");

                    b.ToTable("game_retailer", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Genre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("normalized_name");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_genre");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("ix_genre_normalized_name")
                        .HasAnnotation("Npgsql:CreatedConcurrently", true);

                    b.ToTable("genre", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Platform", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("normalized_name");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_platform");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("ix_platform_normalized_name")
                        .HasAnnotation("Npgsql:CreatedConcurrently", true);

                    b.ToTable("platform", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("normalized_name");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_publisher");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("ix_publisher_normalized_name")
                        .HasAnnotation("Npgsql:CreatedConcurrently", true);

                    b.ToTable("publisher", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Retailer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("text")
                        .HasColumnName("normalized_name");

                    b.Property<uint>("xmin")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("xid")
                        .HasColumnName("xmin");

                    b.HasKey("Id")
                        .HasName("pk_retailer");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("ix_retailer_normalized_name")
                        .HasAnnotation("Npgsql:CreatedConcurrently", true);

                    b.ToTable("retailer", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_name");

                    b.HasKey("Id")
                        .HasName("pk_role");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("Id")
                        .HasName("pk_role_claim");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_role_claim_role_id");

                    b.ToTable("role_claim", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer")
                        .HasColumnName("access_failed_count");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text")
                        .HasColumnName("concurrency_stamp");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(30)
                        .IsUnicode(true)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("display_name");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("email");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("email_confirmed");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("lockout_enabled");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("lockout_end");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_email");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("normalized_user_name");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean")
                        .HasColumnName("phone_number_confirmed");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text")
                        .HasColumnName("security_stamp");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_factor_enabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("user_name");

                    b.HasKey("Id")
                        .HasName("pk_user");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text")
                        .HasColumnName("claim_type");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text")
                        .HasColumnName("claim_value");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_claim");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_claim_user_id");

                    b.ToTable("user_claim", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.UserLogin", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text")
                        .HasColumnName("provider_key");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text")
                        .HasColumnName("provider_display_name");

                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.HasKey("LoginProvider", "ProviderKey")
                        .HasName("pk_user_login");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_login_user_id");

                    b.ToTable("user_login", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("integer")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("pk_user_role");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("ix_user_role_role_id");

                    b.ToTable("user_role", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.UserToken", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text")
                        .HasColumnName("login_provider");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Value")
                        .HasColumnType("text")
                        .HasColumnName("value");

                    b.HasKey("UserId", "LoginProvider", "Name")
                        .HasName("pk_user_token");

                    b.ToTable("user_token", (string)null);
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GameEndorsement", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Game", "Game")
                        .WithMany("Endorsements")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_game_endorsement_game_game_id");

                    b.HasOne("GCP.RazorPagesApp.Data.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_game_endorsement_users_user_id");

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GameGenre", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Game", "Game")
                        .WithMany("GameGenres")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_game_genre_game_game_id");

                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Genre", "Genre")
                        .WithMany("GameGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_game_genre_genre_genre_id");

                    b.Navigation("Game");

                    b.Navigation("Genre");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GameLink", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Game", "Game")
                        .WithMany("GameLinks")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_game_link_game_game_id");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GamePublisher", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Game", "Game")
                        .WithMany("GamePublishers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_game_publisher_game_game_id");

                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Publisher", "Publisher")
                        .WithMany("GamePublishers")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_game_publisher_publisher_publisher_id");

                    b.Navigation("Game");

                    b.Navigation("Publisher");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GamePublisherPlatform", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Game", null)
                        .WithMany("GamePublisherPlatforms")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_game_publisher_platform_game_game_id");

                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Platform", "Platform")
                        .WithMany("GamePublisherPlatforms")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_game_publisher_platform_platform_platform_id");

                    b.HasOne("GCP.RazorPagesApp.Data.Entities.GamePublisher", "GamePublisher")
                        .WithMany("GamePublisherPlatforms")
                        .HasForeignKey("GameId", "PublisherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_game_publisher_platform_game_publisher_game_publisher_temp_");

                    b.Navigation("GamePublisher");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GameRetailer", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Game", "Game")
                        .WithMany("GameRetailers")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_game_retailer_game_game_id");

                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Retailer", "Retailer")
                        .WithMany("GameRetailers")
                        .HasForeignKey("RetailerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired()
                        .HasConstraintName("fk_game_retailer_retailer_retailer_id");

                    b.Navigation("Game");

                    b.Navigation("Retailer");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.RoleClaim", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_role_claim_asp_net_roles_role_id");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.UserClaim", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_claim_asp_net_users_user_id");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.UserLogin", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_login_user_user_id");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.UserRole", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_role_role_id");

                    b.HasOne("GCP.RazorPagesApp.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_role_user_user_id");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.UserToken", b =>
                {
                    b.HasOne("GCP.RazorPagesApp.Data.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_token_user_user_id");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Game", b =>
                {
                    b.Navigation("Endorsements");

                    b.Navigation("GameGenres");

                    b.Navigation("GameLinks");

                    b.Navigation("GamePublisherPlatforms");

                    b.Navigation("GamePublishers");

                    b.Navigation("GameRetailers");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.GamePublisher", b =>
                {
                    b.Navigation("GamePublisherPlatforms");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Genre", b =>
                {
                    b.Navigation("GameGenres");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Platform", b =>
                {
                    b.Navigation("GamePublisherPlatforms");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Publisher", b =>
                {
                    b.Navigation("GamePublishers");
                });

            modelBuilder.Entity("GCP.RazorPagesApp.Data.Entities.Retailer", b =>
                {
                    b.Navigation("GameRetailers");
                });
#pragma warning restore 612, 618
        }
    }
}
