using System.ComponentModel.DataAnnotations;

namespace Library.Domains
{
    public class Usuario
    {
        [Key]
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }

    }
}
