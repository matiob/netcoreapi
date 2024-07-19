using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Domains
{
    public class Genero
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string? Nombre { get; set; }
        
        // Colección de Libros (relacion de tablas)
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
