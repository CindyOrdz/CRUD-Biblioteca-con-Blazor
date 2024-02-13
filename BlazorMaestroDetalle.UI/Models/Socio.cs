using System.ComponentModel.DataAnnotations;

namespace BlazorMaestroDetalle.UI.Models
{
    public class Socio
    {
        [Required(ErrorMessage = "El campo ID es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo apellidos es obligatorio.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo telefono es obligatorio.")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El campo correo es obligatorio.")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria.")]
        public DateTime Ingreso { get; set; }

        public string RutaImagen { get; set; }

        public IFormFile Foto { get; set; }


    }
}
