using PC2.Models;

public class InmuebleVisitaViewModel
{
    public Inmueble Inmueble { get; set; }
    public DateTime? FechaInicio { get; set; } // Hacer nullable
    public DateTime? FechaFin { get; set; }   // Hacer nullable
    public string Notas { get; set; }
}
public class VisitaViewModel
{
    public int InmuebleId { get; set; }
    public DateTime? FechaInicio { get; set; } // Hacer nullable
    public DateTime? FechaFin { get; set; }   // Hacer nullable
    public string Notas { get; set; }
}