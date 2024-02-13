namespace BlazorMaestroDetalle.UI.Models
{
    public class Prestamo
    {
        public Libro libro { get; set; }
        public Socio socio { get; set; }
        public DateTime fprestamo { get; set; }
        public DateTime fdevolucion { get; set; }

        public int cantidad { get; set; }
    }
}
