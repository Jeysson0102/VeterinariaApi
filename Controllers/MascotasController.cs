using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VeterinariaApi.Models;
using VeterinariaApi.DTOs;

namespace VeterinariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAll() 
            => Ok(new ApiResponse<object>(true, "Mascotas recuperadas", DataStore.Mascotas));

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == id);
            return mascota == null 
                ? NotFound(new ApiResponse<object>(false, $"No se encontró la mascota con ID {id}.")) 
                : Ok(new ApiResponse<object>(true, "Mascota encontrada", mascota));
        }

        [HttpPost]
        public IActionResult Create([FromBody] MascotaDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(false, "Datos inválidos", ModelState));

            var nuevaMascota = new Mascota {
                Id = DataStore.Mascotas.Count > 0 ? DataStore.Mascotas.Max(m => m.Id) + 1 : 1,
                Nombre = dto.Nombre,
                Especie = dto.Especie
            };
            DataStore.Mascotas.Add(nuevaMascota);
            return CreatedAtAction(nameof(GetById), new { id = nuevaMascota.Id }, new ApiResponse<object>(true, "Mascota creada", nuevaMascota));
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MascotaDTO dto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(false, "Datos inválidos", ModelState));

            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == id);
            if (mascota == null) return NotFound(new ApiResponse<object>(false, "Mascota no encontrada."));
            
            mascota.Nombre = dto.Nombre;
            mascota.Especie = dto.Especie;
            return Ok(new ApiResponse<object>(true, "Mascota actualizada", mascota));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == id);
            if (mascota == null) return NotFound(new ApiResponse<object>(false, "Mascota no encontrada."));
            
            DataStore.Mascotas.Remove(mascota);
            return Ok(new ApiResponse<object>(true, "Mascota eliminada"));
        }
    }
}