﻿// <auto-generated />
using System;
using APOYOS_SOCIALES;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APOYOSSOCIALES.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.ActiveToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("ExpirationTime")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("TokenId")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Activetokens");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Adquisicion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<string>("FechaAdquisicion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Folio")
                        .HasColumnType("longtext");

                    b.Property<string>("Marca")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("PrecioTotal")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Proveedor")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Adquisiciones");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Apoyo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("CURP")
                        .HasColumnType("longtext");

                    b.Property<string>("Comentarios")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ComunidadId")
                        .HasColumnType("int");

                    b.Property<string>("Edad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<decimal>("Latitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Longitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("ComunidadId");

                    b.HasIndex("GeneroId");

                    b.ToTable("Apoyos");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Area", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Icono")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Cargo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cargos");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Claim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("ClaimValue")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RolId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Comunidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Comunidades");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Fondo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<decimal>("Cantidad")
                        .HasColumnType("decimal(65,30)");

                    b.Property<string>("Periodo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("TipoDistribucionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoDistribucionId");

                    b.ToTable("Fondos");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Genero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Generos");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Incidencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comentarios")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("ComunidadId")
                        .HasColumnType("int");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Latitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal>("Longitud")
                        .HasColumnType("decimal(65,30)");

                    b.Property<int>("TipoIncidenciaId")
                        .HasColumnType("int");

                    b.Property<string>("Ubicacion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ComunidadId");

                    b.HasIndex("TipoIncidenciaId");

                    b.ToTable("Incidencias");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Personal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<int>("CargoId")
                        .HasColumnType("int");

                    b.Property<string>("Edad")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FechaContratacion")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("GeneroId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<decimal>("Salario")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("CargoId");

                    b.HasIndex("GeneroId");

                    b.ToTable("Personales");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.ProgramaSocial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AreaId")
                        .HasColumnType("int");

                    b.Property<bool>("Estatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.ToTable("Programassociales");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NombreRol")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Rols");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.TipoDistribucion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TiposDistribuciones");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.TipoIncidencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Icono")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("TiposIncidencias");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AreaId")
                        .HasColumnType("int");

                    b.Property<string>("Correo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<bool>("Estatus")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("NombreCompleto")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("RolId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Adquisicion", b =>
                {
                    b.HasOne("APOYOS_SOCIALES.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Apoyo", b =>
                {
                    b.HasOne("APOYOS_SOCIALES.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APOYOS_SOCIALES.Entities.Comunidad", "Comunidad")
                        .WithMany()
                        .HasForeignKey("ComunidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APOYOS_SOCIALES.Entities.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("Comunidad");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Claim", b =>
                {
                    b.HasOne("APOYOS_SOCIALES.Entities.Rol", "Rol")
                        .WithMany("Claims")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Fondo", b =>
                {
                    b.HasOne("APOYOS_SOCIALES.Entities.TipoDistribucion", "TipoDistribucion")
                        .WithMany()
                        .HasForeignKey("TipoDistribucionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TipoDistribucion");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Incidencia", b =>
                {
                    b.HasOne("APOYOS_SOCIALES.Entities.Comunidad", "Comunidad")
                        .WithMany()
                        .HasForeignKey("ComunidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APOYOS_SOCIALES.Entities.TipoIncidencia", "TipoIncidencia")
                        .WithMany()
                        .HasForeignKey("TipoIncidenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comunidad");

                    b.Navigation("TipoIncidencia");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Personal", b =>
                {
                    b.HasOne("APOYOS_SOCIALES.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId");

                    b.HasOne("APOYOS_SOCIALES.Entities.Cargo", "Cargo")
                        .WithMany()
                        .HasForeignKey("CargoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("APOYOS_SOCIALES.Entities.Genero", "Genero")
                        .WithMany()
                        .HasForeignKey("GeneroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("Cargo");

                    b.Navigation("Genero");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.ProgramaSocial", b =>
                {
                    b.HasOne("APOYOS_SOCIALES.Entities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Usuario", b =>
                {
                    b.HasOne("APOYOS_SOCIALES.Entities.Area", "Area")
                        .WithMany("Usuarios")
                        .HasForeignKey("AreaId");

                    b.HasOne("APOYOS_SOCIALES.Entities.Rol", "Rol")
                        .WithMany("Usuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Area");

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Area", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("APOYOS_SOCIALES.Entities.Rol", b =>
                {
                    b.Navigation("Claims");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
