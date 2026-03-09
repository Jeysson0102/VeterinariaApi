using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VeterinariaApi.Models;
using VeterinariaApi.DTOs;

namespace VeterinariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeterinariosController : ControllerBase
    {
        // GET: api/veterinarios (Listar todos)
        [HttpGet]
        public IActionResult GetAll() => Ok(DataStore.Veterinarios);

        // GET: api/veterinarios/1 (Obtener por ID)
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == id);
            if (veterinario == null) 
                return NotFound(new { mensaje = $"No se encontró el veterinario con ID {id}." });
            
            return Ok(veterinario);
        }

        // POST: api/veterinarios (Crear nuevo)
        [HttpPost]
        public IActionResult Create([FromBody] VeterinarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var nuevoVeterinario = new Veterinario
            {
                Id = DataStore.Veterinarios.Count > 0 ? DataStore.Veterinarios.Max(v => v.Id) + 1 : 1,
                Nombre = dto.Nombre,
                Especialidad = dto.Especialidad
            };

            DataStore.Veterinarios.Add(nuevoVeterinario);
            return CreatedAtAction(nameof(GetById), new { id = nuevoVeterinario.Id }, nuevoVeterinario);
        }

        // PUT: api/veterinarios/1 (Actualizar)
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] VeterinarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == id);
            if (veterinario == null) 
                return NotFound(new { mensaje = "Veterinario no encontrado para actualizar." });
            
            veterinario.Nombre = dto.Nombre;
            veterinario.Especialidad = dto.Especialidad;

            return NoContent(); // Estándar profesional para actualizaciones exitosas
        }

        // DELETE: api/veterinarios/1 (Eliminar)
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == id);
            if (veterinario == null) 
                return NotFound(new { mensaje = "Veterinario no encontrado para eliminar." });
            
            DataStore.Veterinarios.Remove(veterinario);
            return NoContent(); // Estándar profesional para eliminaciones exitosas
        }
    }
}