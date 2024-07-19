using Library.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.DTOs.Response
{
    public class AutorDTO
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
        public required string FechaNacimiento { get; set; }
        public required string Pais { get; set; }
    }
}
