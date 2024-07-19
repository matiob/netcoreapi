using Library.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.DTOs.Response
{
    public class LibroDTO
    {
        public required string Id { get; set; }
        public string? ISBN { get; set; }
        public required string Titulo { get; set; }
        public string? FechaPublicacion { get; set; }
        public required string Autor { get; set; }
        public required string Genero { get; set; }
    }
}
