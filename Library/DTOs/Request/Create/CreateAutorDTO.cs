namespace Library.DTOs.Request.Create
{
    public class CreateAutorDTO
    {
        public required string Nombre { get; set; }
        public required string FechaNacimiento { get; set; }
        public string? Email { get; set; }
        public string PaisId { get; set; }
    }
}
