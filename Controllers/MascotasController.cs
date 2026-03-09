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
        public IActionResult GetAll() => Ok(DataStore.Mascotas);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == id);
            return mascota == null ? NotFound() : Ok(mascota);
        }

        [HttpPost]
        public IActionResult Create([FromBody] MascotaDTO dto)
        {
            var nuevaMascota = new Mascota {
                Id = DataStore.Mascotas.Count > 0 ? DataStore.Mascotas.Max(m => m.Id) + 1 : 1,
                Nombre = dto.Nombre,
                Especie = dto.Especie
            };
            DataStore.Mascotas.Add(nuevaMascota);
            return CreatedAtAction(nameof(GetById), new { id = nuevaMascota.Id }, nuevaMascota);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MascotaDTO dto)
        {
            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == id);
            if (mascota == null) return NotFound();
            
            mascota.Nombre = dto.Nombre;
            mascota.Especie = dto.Especie;
            return NoContent(); // 204 No Content es estándar profesional para PUT
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == id);
            if (mascota == null) return NotFound();
            
            DataStore.Mascotas.Remove(mascota);
            return NoContent();
        }
    }
}