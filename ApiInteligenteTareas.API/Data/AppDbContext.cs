using Microsoft.EntityFrameworkCore;
using ApiInteligenteTareas.API.Models;

namespace ApiInteligenteTareas.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Tarea> Tareas { get; set; }
}