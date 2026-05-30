using ApiInteligenteTareas.API.DTOs;
using ApiInteligenteTareas.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiInteligenteTareas.API.Controllers;

[ApiController]
[Route("api/tareas-externas")]
public class TareasExternasController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TareasExternasController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TareaExternaDto>>> GetTareasExternas()
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var tareas = await client.GetFromJsonAsync<List<TodoExterno>>(
                "https://jsonplaceholder.typicode.com/todos");

            if (tareas == null)
                return StatusCode(500, "No se pudo obtener información.");

            var resultado = tareas.Select(t => new TareaExternaDto
            {
                ExternalId = t.Id,
                Titulo = t.Title,
                Completado = t.Completed
            });

            return Ok(resultado);
        }
        catch
        {
            return StatusCode(500, "Error al consumir la API externa.");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TareaExternaDto>> GetTareaExterna(int id)
    {
        try
        {
            var client = _httpClientFactory.CreateClient();

            var tarea = await client.GetFromJsonAsync<TodoExterno>(
                $"https://jsonplaceholder.typicode.com/todos/{id}");

            if (tarea == null)
                return NotFound();

            return Ok(new TareaExternaDto
            {
                ExternalId = tarea.Id,
                Titulo = tarea.Title,
                Completado = tarea.Completed
            });
        }
        catch
        {
            return NotFound();
        }
    }
}