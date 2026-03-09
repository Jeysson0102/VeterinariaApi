using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VeterinariaApi.Models;
using VeterinariaApi.DTOs;

namespace VeterinariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        // GET: api/citas (Listar todas)
        [HttpGet]
        public IActionResult GetCitas()
        {
            return Ok(DataStore.Citas);
        }

        // GET: api/citas/1 (Obtener por ID)
        [HttpGet("{id}")]
        public IActionResult GetCita(int id)
        {
            var cita = DataStore.Citas.FirstOrDefault(c => c.Id == id);
            if (cita == null) 
                return NotFound(new { mensaje = $"No se encontró la cita con el ID {id}." });
            
            return Ok(cita);
        }

        // POST: api/citas (Registrar usando IDs de Mascota y Veterinario)
        [HttpPost]
        public IActionResult CrearCita([FromBody] CitaDTO citaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Buscamos si la mascota y el veterinario existen en nuestros registros centrales
            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == citaDto.MascotaId);
            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == citaDto.VeterinarioId);

            if (mascota == null) 
                return NotFound(new { mensaje = $"La mascota con ID {citaDto.MascotaId} no existe." });
            if (veterinario == null) 
                return NotFound(new { mensaje = $"El veterinario con ID {citaDto.VeterinarioId} no existe." });

            // Armamos el objeto real con la información completa
            var nuevaCita = new CitaMascota
            {
                Id = DataStore.Citas.Count > 0 ? DataStore.Citas.Max(c => c.Id) + 1 : 1,
                Fecha = citaDto.Fecha,
                Motivo = citaDto.Motivo,
                Mascota = mascota,
                Veterinario = veterinario
            };
            
            DataStore.Citas.Add(nuevaCita);
            return CreatedAtAction(nameof(GetCita), new { id = nuevaCita.Id }, nuevaCita);
        }

        // PUT: api/citas/1 (Actualizar cita existente)
        [HttpPut("{id}")]
        public IActionResult ActualizarCita(int id, [FromBody] CitaDTO citaDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var citaExistente = DataStore.Citas.FirstOrDefault(c => c.Id == id);
            if (citaExistente == null) 
                return NotFound(new { mensaje = "Cita no encontrada para actualizar." });

            // Validamos que los nuevos IDs de relación existan
            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == citaDto.MascotaId);
            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == citaDto.VeterinarioId);

            if (mascota == null || veterinario == null)
                return BadRequest(new { mensaje = "Mascota o Veterinario no encontrados." });

            // Actualizamos los campos
            citaExistente.Fecha = citaDto.Fecha;
            citaExistente.Motivo = citaDto.Motivo;
            citaExistente.Mascota = mascota;
            citaExistente.Veterinario = veterinario;

            return Ok(new { mensaje = "Cita actualizada correctamente.", cita = citaExistente });
        }

        // DELETE: api/citas/1 (Eliminar cita)
        [HttpDelete("{id}")]
        public IActionResult EliminarCita(int id)
        {
            var cita = DataStore.Citas.FirstOrDefault(c => c.Id == id);
            if (cita == null) 
                return NotFound(new { mensaje = "Cita no encontrada para eliminar." });

            DataStore.Citas.Remove(cita);
            return Ok(new { mensaje = "Cita eliminada correctamente." });
        }
    }
}