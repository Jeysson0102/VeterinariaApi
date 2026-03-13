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
        [HttpGet]
        public IActionResult GetAll() 
            => Ok(new ApiResponse<object>(true, "Veterinarios recuperados", DataStore.Veterinarios));

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == id);
            if (veterinario == null) 
                return NotFound(new ApiResponse<object>(false, $"No se encontró el veterinario con ID {id}."));
            
            return Ok(new ApiResponse<object>(true, "Veterinario encontrado", veterinario));
        }

        [HttpPost]
        public IActionResult Create([FromBody] VeterinarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(false, "Datos inválidos", ModelState));

            var nuevoVeterinario = new Veterinario
            {
                Id = DataStore.Veterinarios.Count > 0 ? DataStore.Veterinarios.Max(v => v.Id) + 1 : 1,
                Nombre = dto.Nombre,
                Especialidad = dto.Especialidad
            };

            DataStore.Veterinarios.Add(nuevoVeterinario);
            return CreatedAtAction(nameof(GetById), new { id = nuevoVeterinario.Id }, new ApiResponse<object>(true, "Veterinario creado", nuevoVeterinario));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] VeterinarioDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(false, "Datos inválidos", ModelState));

            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == id);
            if (veterinario == null) 
                return NotFound(new ApiResponse<object>(false, "Veterinario no encontrado."));
            
            veterinario.Nombre = dto.Nombre;
            veterinario.Especialidad = dto.Especialidad;

            return Ok(new ApiResponse<object>(true, "Veterinario actualizado", veterinario));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == id);
            if (veterinario == null) 
                return NotFound(new ApiResponse<object>(false, "Veterinario no encontrado."));
            
            DataStore.Veterinarios.Remove(veterinario);
            return Ok(new ApiResponse<object>(true, "Veterinario eliminado"));
        }
    }
}