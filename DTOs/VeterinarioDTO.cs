using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.DTOs
{
    public class VeterinarioDTO
    {
        [Required(ErrorMessage = "El nombre del veterinario es obligatorio.")]
        [StringLength(100)]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        public required string Especialidad { get; set; }
    }
}