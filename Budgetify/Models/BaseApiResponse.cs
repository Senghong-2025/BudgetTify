using System.Text.Json.Serialization;

namespace Budgetify.Models;

public class BaseApiResponse<T>
{
    public int ErrorCode { get; set; } = 0;
    public string ErrorMessage { get; set; }
    public T Data { get; set; }
    public bool IsSuccess => ErrorCode == 0;
    public BaseApiResponse(T data)
    {
        Data = data;
    }
    public BaseApiResponse(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
    public BaseApiResponse(int errorCode, string errorMessage, T data)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        Data = data;
    }
}