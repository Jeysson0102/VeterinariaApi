using System;
using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.Models
{
    public class CitaMascota : BaseEntity
    {
        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public required string Motivo { get; set; }

        [Required]
        public required Mascota Mascota { get; set; }

        [Required]
        public required Veterinario Veterinario { get; set; }
    }
}