using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domains
{
    public class Libro
    {
        [Key]
        public Guid Id { get; set; }
        public string? ISBN { get; set; }
        [MaxLength(100)]
        [Required]
        public string Titulo { get; set; } = string.Empty;
        public DateTime? FechaPublicacion { get; set; }

        // Clave foránea para Autor
        [ForeignKey("AutorId")]
        public Guid AutorId { get; set; }
        public Autor Autor { get; set; }

        // Clave foránea para Genero
        [ForeignKey("GeneroId")]
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
    }
}
