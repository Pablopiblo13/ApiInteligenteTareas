using System.ComponentModel.DataAnnotations;

namespace ApiInteligenteTareas.API.Models;

public class Tarea
{
    public int Id { get; set; }

    [Required]
    public string Titulo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    [Required]
    public EstadoTarea Estado { get; set; }

    [Required]
    public PrioridadTarea Prioridad { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.Now;

    [Required]
    public DateTime FechaVencimiento { get; set; }
}