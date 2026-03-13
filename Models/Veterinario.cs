using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.Models
{
    public class Veterinario : BaseEntity
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La especialidad es obligatoria.")]
        public required string Especialidad { get; set; }
    }
}