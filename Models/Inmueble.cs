using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PC2.Models
{
    public class Inmueble
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Codigo { get; set; } // Código único

        [Required]
        [StringLength(200)]
        public string Titulo { get; set; }

        public string Imagen { get; set; } // URL de la imagen o base64

        [Required]
        public TipoInmueble Tipo { get; set; }

        [Required]
        [StringLength(100)]
        public string Ciudad { get; set; }

        [Required]
        [StringLength(255)]
        public string Direccion { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de dormitorios debe ser mayor que 0.")]
        public int Dormitorios { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de baños debe ser mayor que 0.")]
        public int Banos { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Los metros cuadrados deben ser mayores que 0.")]
        public double MetrosCuadrados { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor que 0.")]
        public decimal Precio { get; set; }

        public bool Activo { get; set; }
        public bool ReservaActiva { get; set; }

        // Relación con las reservas (suponiendo que una reserva tiene un campo FechaExpiracion)
        public virtual ICollection<Reserva> Reservas { get; set; }
    }

    public enum TipoInmueble
    {
        Departamento,
        Casa,
        Oficina,
        Local
    }

}