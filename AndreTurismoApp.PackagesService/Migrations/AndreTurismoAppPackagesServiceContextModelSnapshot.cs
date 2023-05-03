﻿// <auto-generated />
using AndreTurismoApp.PackagesService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AndreTurismoApp.PackagesService.Migrations
{
    [DbContext(typeof(AndreTurismoAppPackagesServiceContext))]
    partial class AndreTurismoAppPackagesServiceContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AndreTurismoApp.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CEP")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdCityId")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCityId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.City", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("City");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdAddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdAddressId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdAddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdAddressId");

                    b.ToTable("Hotel");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Package", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdCustomerId")
                        .HasColumnType("int");

                    b.Property<int>("IdHotelId")
                        .HasColumnType("int");

                    b.Property<int>("IdTicketId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdCustomerId");

                    b.HasIndex("IdHotelId");

                    b.HasIndex("IdTicketId");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdCustomerId")
                        .HasColumnType("int");

                    b.Property<int>("IdDestinationId")
                        .HasColumnType("int");

                    b.Property<int>("IdOriginId")
                        .HasColumnType("int");

                    b.Property<decimal>("Value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("IdCustomerId");

                    b.HasIndex("IdDestinationId");

                    b.HasIndex("IdOriginId");

                    b.ToTable("Ticket");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Address", b =>
                {
                    b.HasOne("AndreTurismoApp.Models.City", "IdCity")
                        .WithMany()
                        .HasForeignKey("IdCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdCity");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Customer", b =>
                {
                    b.HasOne("AndreTurismoApp.Models.Address", "IdAddress")
                        .WithMany()
                        .HasForeignKey("IdAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdAddress");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Hotel", b =>
                {
                    b.HasOne("AndreTurismoApp.Models.Address", "IdAddress")
                        .WithMany()
                        .HasForeignKey("IdAddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdAddress");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Package", b =>
                {
                    b.HasOne("AndreTurismoApp.Models.Customer", "IdCustomer")
                        .WithMany()
                        .HasForeignKey("IdCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AndreTurismoApp.Models.Hotel", "IdHotel")
                        .WithMany()
                        .HasForeignKey("IdHotelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AndreTurismoApp.Models.Ticket", "IdTicket")
                        .WithMany()
                        .HasForeignKey("IdTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdCustomer");

                    b.Navigation("IdHotel");

                    b.Navigation("IdTicket");
                });

            modelBuilder.Entity("AndreTurismoApp.Models.Ticket", b =>
                {
                    b.HasOne("AndreTurismoApp.Models.Customer", "IdCustomer")
                        .WithMany()
                        .HasForeignKey("IdCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AndreTurismoApp.Models.Address", "IdDestination")
                        .WithMany()
                        .HasForeignKey("IdDestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AndreTurismoApp.Models.Address", "IdOrigin")
                        .WithMany()
                        .HasForeignKey("IdOriginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdCustomer");

                    b.Navigation("IdDestination");

                    b.Navigation("IdOrigin");
                });
#pragma warning restore 612, 618
        }
    }
}
