using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.Models
{
    public class Mascota
    {
        [Required(ErrorMessage = "El ID de la mascota es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La especie es obligatoria (ej. Perro, Gato).")]
        public required string Especie { get; set; }
    }
}