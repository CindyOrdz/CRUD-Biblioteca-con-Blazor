using System.ComponentModel.DataAnnotations;

namespace BlazorMaestroDetalle.UI.Models
{
    public class Libro
    {
        [Required(ErrorMessage = "El campo ID es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Título es obligatorio.")]
        [StringLength(250, ErrorMessage = "El titulo solo puede contener 250 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El campo Autor es obligatorio.")]
        [StringLength(250, ErrorMessage = "El nombre del autor solo puede contener 250 caracteres")]
        public string Autor { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El número de páginas debe ser mayor que cero.")]
        public int NumPag { get; set; }
        
    }
}
