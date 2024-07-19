using Library.DTOs.Response;
using System.Net;

namespace Library.Controllers
{
    public static class ApiResponseWrapper
    {
        public static ApiResponse<T> CreateResponse<T>(T data, HttpStatusCode status = HttpStatusCode.BadRequest, string errorMessage = "Hubo un error en la solicitud")
        {
            if (data != null)
            {
                return new ApiResponse<T>
                {
                    Status = HttpStatusCode.OK,
                    Data = data,
                };
            }
            else
            {
                return new ApiResponse<T>
                {
                    Status = status,
                    ErrorMessage = errorMessage
                };
            }
        }
    }
}
