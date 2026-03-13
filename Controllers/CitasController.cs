using Microsoft.AspNetCore.Mvc;
using System.Linq;
using VeterinariaApi.Models;
using VeterinariaApi.DTOs;

namespace VeterinariaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCitas()
        {
            return Ok(new ApiResponse<object>(true, "Visualizando Citas", DataStore.Citas));
        }

        [HttpGet("{id}")]
        public IActionResult GetCita(int id)
        {
            var cita = DataStore.Citas.FirstOrDefault(c => c.Id == id);
            if (cita == null) 
                return NotFound(new ApiResponse<object>(false, $"No se encontró la cita con ID {id}."));
            
            return Ok(new ApiResponse<object>(true, "Cita encontrada", cita));
        }

        // RUTA JERÁRQUICA: /api/citas/mascota/{mascotaId}
        [HttpGet("mascota/{mascotaId}")]
        public IActionResult GetCitasPorMascota(int mascotaId)
        {
            var citasMascota = DataStore.Citas.Where(c => c.Mascota.Id == mascotaId).ToList();
            if (!citasMascota.Any())
                return NotFound(new ApiResponse<object>(false, $"No hay citas para la mascota {mascotaId}."));

            return Ok(new ApiResponse<object>(true, "Citas encontradas", citasMascota));
        }

        [HttpPost]
        public IActionResult CrearCita([FromBody] CitaDTO citaDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(false, "Datos inválidos", ModelState));

            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == citaDto.MascotaId);
            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == citaDto.VeterinarioId);

            if (mascota == null || veterinario == null) 
                return NotFound(new ApiResponse<object>(false, "Mascota o Veterinario no existen."));

            var nuevaCita = new CitaMascota
            {
                Id = DataStore.Citas.Count > 0 ? DataStore.Citas.Max(c => c.Id) + 1 : 1,
                Fecha = citaDto.Fecha,
                Motivo = citaDto.Motivo,
                Mascota = mascota,
                Veterinario = veterinario
            };
            
            DataStore.Citas.Add(nuevaCita);
            return CreatedAtAction(nameof(GetCita), new { id = nuevaCita.Id }, new ApiResponse<object>(true, "Cita creada", nuevaCita));
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarCita(int id, [FromBody] CitaDTO citaDto)
        {
            if (!ModelState.IsValid) return BadRequest(new ApiResponse<object>(false, "Datos inválidos", ModelState));

            var citaExistente = DataStore.Citas.FirstOrDefault(c => c.Id == id);
            if (citaExistente == null) 
                return NotFound(new ApiResponse<object>(false, "Cita no encontrada."));

            var mascota = DataStore.Mascotas.FirstOrDefault(m => m.Id == citaDto.MascotaId);
            var veterinario = DataStore.Veterinarios.FirstOrDefault(v => v.Id == citaDto.VeterinarioId);

            if (mascota == null || veterinario == null)
                return BadRequest(new ApiResponse<object>(false, "Mascota o Veterinario no existen."));

            citaExistente.Fecha = citaDto.Fecha;
            citaExistente.Motivo = citaDto.Motivo;
            citaExistente.Mascota = mascota;
            citaExistente.Veterinario = veterinario;

            return Ok(new ApiResponse<object>(true, "Cita actualizada", citaExistente));
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarCita(int id)
        {
            var cita = DataStore.Citas.FirstOrDefault(c => c.Id == id);
            if (cita == null) 
                return NotFound(new ApiResponse<object>(false, "Cita no encontrada."));

            DataStore.Citas.Remove(cita);
            return Ok(new ApiResponse<object>(true, "Cita eliminada"));
        }
    }
}