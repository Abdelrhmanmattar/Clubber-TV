using code_quests.Core.entities;
using code_quests.Core.Enum;
using System.Net;

namespace code_quests.Core.DTOs
{
    public class ResponseDTO
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
        public ResponseDTO(HttpStatusCode statusCode, string msg = null, object dt = null)
        {
            StatusCode = statusCode;
            Message = msg;
            Data = dt;
        }
    }
}
