namespace VeterinariaApi.Models
{
    public class ApiResponse<T>
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public T? Datos { get; set; }

        public ApiResponse(bool exito, string mensaje, T? datos = default)
        {
            Exito = exito;
            Mensaje = mensaje;
            Datos = datos;
        }
    }
}