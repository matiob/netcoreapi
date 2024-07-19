using System.Net;

namespace Library.DTOs.Response
{
    public class ApiResponse<T>
    {
        public HttpStatusCode Status { get; set; }
        public T? Data { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
