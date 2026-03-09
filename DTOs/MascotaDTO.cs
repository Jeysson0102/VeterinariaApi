using System.ComponentModel.DataAnnotations;

namespace VeterinariaApi.DTOs
{
    public class MascotaDTO
    {
        [Required(ErrorMessage = "El nombre de la mascota es obligatorio.")]
        [StringLength(50)]
        public required string Nombre { get; set; }

        [Required(ErrorMessage = "La especie es obligatoria.")]
        public required string Especie { get; set; }
    }
}