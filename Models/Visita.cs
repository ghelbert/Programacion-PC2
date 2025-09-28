using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PC2.Models
{
    public class Visita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int InmuebleId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaInicio { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FechaFin { get; set; }

        [Required]
        public EstadoVisita Estado { get; set; }
        [Required]
        public string Notas { get; set; }

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