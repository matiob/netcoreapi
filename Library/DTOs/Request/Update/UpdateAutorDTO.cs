using Library.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Library.DTOs.Request.Update
{
    public class UpdateAutorDTO
    {
        public required string Id { get; set; }
        public required string Nombre { get; set; }
        public required string FechaNacimiento { get; set; }
        public string? Email { get; set; }
        public int PaisId { get; set; }

    }
}
