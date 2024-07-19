using Library.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.DTOs.Request.Update
{
    public class UpdateLibroDTO
    {
        public required string Id { get; set; }
        public string? ISBN { get; set; }
        public required string Titulo { get; set; }
        public string? FechaPublicacion { get; set; }
        public int GeneroId { get; set; }
    }
}
