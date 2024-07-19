namespace Library.DTOs.Request.Client
{
    public class PaisDTO
    {
        public PaisApiNameDto Name { get; set; }
        public string ccn3 { get; set; }
    }

    public class PaisApiNameDto
    {
        public string official { get; set; }
    }
}
