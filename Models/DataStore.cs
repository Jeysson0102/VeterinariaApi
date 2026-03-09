using System.Collections.Generic;

namespace VeterinariaApi.Models
{
    // Esta clase estática simula una base de datos real accesible desde cualquier lugar
    public static class DataStore
    {
        public static List<Mascota> Mascotas = new List<Mascota>
        {
            new Mascota { Id = 1, Nombre = "Luna", Especie = "Gato" }
        };

        public static List<Veterinario> Veterinarios = new List<Veterinario>
        {
            new Veterinario { Id = 1, Nombre = "Dr. Ramírez", Especialidad = "Felinos" }
        };

        public static List<CitaMascota> Citas = new List<CitaMascota>();
    }
}