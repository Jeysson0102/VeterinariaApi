using System;
using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.DTOs
{
    public class CitaDTO
    {
        [Required(ErrorMessage = "La fecha y hora de la cita es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El motivo de la cita es obligatorio.")]
        [StringLength(200)]
        public required string Motivo { get; set; }

        [Required(ErrorMessage = "El ID de la mascota es obligatorio.")]
        public int MascotaId { get; set; }

        [Required(ErrorMessage = "El ID del veterinario es obligatorio.")]
        public int VeterinarioId { get; set; }
    }
}