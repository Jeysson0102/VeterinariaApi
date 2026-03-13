namespace VeterinariaApi.Models
{
    // Abstracción y Herencia: Todas las entidades tendrán un Id obligatorio
    public abstract class BaseEntity
    {
        public int Id { get; set; }
    }
}