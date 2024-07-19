namespace Library.DTOs.Request.Create
{
    public class CreateLibroDTO
    {
        public string? ISBN { get; set; }
        public required string Titulo { get; set; }
        public string? FechaPublicacion { get; set; }
        public required string AutorId { get; set; }
        public int GeneroId { get; set; }
    }
}
