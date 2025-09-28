using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PC2.Models
{
    public class CatalogoViewModel
    {
        // Inmuebles que se mostrarán en la vista
        public IEnumerable<Inmueble> Inmuebles { get; set; }

        // Total de inmuebles para la paginación
        public int TotalInmuebles { get; set; }

        // Número de página actual para la paginación
        public int PageNumber { get; set; }

        // Tamaño de la página (número de inmuebles por página)
        public int PageSize { get; set; }

        // Filtros aplicados (se pueden usar para mantener los valores en la vista)
        public string Ciudad { get; set; }
        public TipoInmueble? Tipo { get; set; }
        public decimal? PrecioMin { get; set; }
        public decimal? PrecioMax { get; set; }
        public int? Dormitorios { get; set; }
    }

}