using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PC2.Models
{
    public class Visita
    {
        public int Id { get; set; }

        [Required]
        public int InmuebleId { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaInicio { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime FechaFin { get; set; }

        [Required]
        public EstadoVisita Estado { get; set; }

        public string Notas { get; set; }

        // Restricción: FechaInicio debe ser antes de FechaFin
        public bool EsFechaValida()
        {
            return FechaInicio < FechaFin;
        }
    }
    public enum EstadoVisita
    {
        Solicitada,
        Confirmada,
        Cancelada
    }
}