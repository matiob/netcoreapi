using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Domains
{
    public class Autor
    {
        [Key]
        public Guid Id { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public string Email { get; set; } = string.Empty;
        
        // Clave foránea para Pais
        // [ForeignKey("PaisId")]
        public string? PaisId { get; set; }
        public Pais? Pais { get; set; }

        // Colección de Libros (relacion de tablas)
        public ICollection<Libro> Libros { get; set; } = new List<Libro>();
    }
}
