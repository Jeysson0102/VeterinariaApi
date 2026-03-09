using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.Models
{
    public class Veterinario
    {
        [Required(ErrorMessage = "El ID del veterinario es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del veterinario es obligatorio.")]
        [StringLength(100)]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        public required string Especialidad { get; set; }
    }
}