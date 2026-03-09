using System;
using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.Models
{
    public class CitaMascota
    {
        public int Id { get; set; } // Este lo autogeneraremos en el controlador

        [Required(ErrorMessage = "La fecha y hora de la cita es obligatoria.")]
        public DateTime Fecha { get; set; }

        [Required(ErrorMessage = "El motivo de la cita es obligatorio.")]
        [StringLength(200)]
        public required string Motivo { get; set; }

        [Required(ErrorMessage = "Los datos de la mascota son obligatorios.")]
        public required Mascota Mascota { get; set; }

        [Required(ErrorMessage = "Los datos del veterinario son obligatorios.")]
        public required Veterinario Veterinario { get; set; }
    }
}