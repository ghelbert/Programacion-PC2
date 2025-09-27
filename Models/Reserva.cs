using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PC2.Models
{
    public class Reserva
    {
        public int Id { get; set; }

        [Required]
        public int InmuebleId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaExpiracion { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaCreacion { get; set; }

        // Restricción: Un inmueble no puede tener más de una reserva activa
        public bool EsReservaValida()
        {
            return FechaExpiracion > DateTime.Now;
        }
    }
}