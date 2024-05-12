using System.Net;

namespace Api.Commons.Models;

public class BaseResponse
{
    public object? Data { get; set; }
    public bool Success { get; set; } = true;
    public string? Message { get; set; } 
    public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    public Array? ErrorMessages { get; set; }
}
