using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PC2.Models;

namespace PC2.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Inmueble> Inmuebles { get; set; }
    public DbSet<Visita> Visitas { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Semilla de datos mínima
        modelBuilder.Entity<Inmueble>().HasData(
            new Inmueble
            {
                Id = 1,
                Codigo = "INM-001",
                Titulo = "Departamento Moderno",
                Tipo = TipoInmueble.Departamento,
                Ciudad = "Madrid",
                Direccion = "Av. Gran Via, 12",
                Imagen = "https://tse4.mm.bing.net/th/id/OIP.RN2FM_PYPFBQL-UxY03gaQHaE8?rs=1&pid=ImgDetMain&o=7&rm=3",
                Dormitorios = 3,
                Banos = 2,
                MetrosCuadrados = 85.5,
                Precio = 200000m,
                Activo = true
            },
            new Inmueble
            {
                Id = 2,
                Codigo = "INM-002",
                Titulo = "Casa de Lujo",
                Tipo = TipoInmueble.Casa,
                Ciudad = "Barcelona",
                Direccion = "Carrer de Pau, 5",
                Dormitorios = 4,
                Imagen = "https://tse4.mm.bing.net/th/id/OIP.JNgdoxHcMPEFZm-MzpH1tgHaFL?rs=1&pid=ImgDetMain&o=7&rm=3",
                Banos = 3,
                MetrosCuadrados = 200,
                Precio = 450000m,
                Activo = true
            },
            new Inmueble
            {
                Id = 3,
                Codigo = "INM-003",
                Titulo = "Oficina Comercial",
                Tipo = TipoInmueble.Oficina,
                Ciudad = "Valencia",
                Direccion = "Carrer del Mar, 1",
                Imagen = "https://www.rescombuilds.com/wp-content/uploads/2022/06/3-exquisicare-senior-living-210415112025-78.jpg",
                Dormitorios = 0,
                Banos = 1,
                MetrosCuadrados = 120,
                Precio = 150000m,
                Activo = true
            },
            new Inmueble
            {
                Id = 4,
                Codigo = "INM-004",
                Titulo = "Local Comercial",
                Tipo = TipoInmueble.Local,
                Ciudad = "Sevilla",
                Direccion = "Calle San Pedro, 8",
                Imagen = "https://www.rescombuilds.com/wp-content/uploads/2022/06/3-exquisicare-senior-living-210415112025-78.jpg",
                Dormitorios = 0,
                Banos = 1,
                MetrosCuadrados = 90,
                Precio = 180000m,
                Activo = true
            }
        );
    }
}
