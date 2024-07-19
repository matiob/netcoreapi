using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.Domains
{
    public class Pais
    {
        // [Key]
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Nombre { get; set; }

        // Colección de Autores
        // public ICollection<Autor> Autores { get; set; } = new List<Autor>();
    }
}
