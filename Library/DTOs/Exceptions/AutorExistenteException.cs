namespace Library.DTOs.Exceptions
{
    public class AutorExistenteException : Exception
    {
        public AutorExistenteException(string nombre) : base($"Ya existe un autor con el nombre '{nombre}'.")
        {

        }
    }
}
