using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.Models
{
    public class Mascota : BaseEntity
    {
        [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La especie es obligatoria (ej. Perro, Gato).")]
        public required string Especie { get; set; }
    }
}