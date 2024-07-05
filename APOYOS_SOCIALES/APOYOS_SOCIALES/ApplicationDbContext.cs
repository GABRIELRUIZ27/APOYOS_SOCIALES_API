using APOYOS_SOCIALES.DTOs;
using APOYOS_SOCIALES.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using SystemClaim = System.Security.Claims.Claim;

namespace APOYOS_SOCIALES
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ActiveToken> Activetokens { get; set; }
        public DbSet<Comunidad> Comunidades { get; set; }
        public DbSet<ProgramaSocial> Programassociales { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<Area> Areas { get; set; } 
        public DbSet<Apoyo> Apoyos { get; set; }
        public DbSet<TipoIncidencia> TiposIncidencias { get; set; }
        public DbSet<Genero> Generos { get; set; } 
        public DbSet<Incidencia> Incidencias { get; set; }
        public DbSet<Personal> Personales { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<TipoDistribucion> TiposDistribuciones { get; set; }
        public DbSet<Fondo> Fondos { get; set; }
        public DbSet<Adquisicion> Adquisiciones { get; set; } 

    }
}
