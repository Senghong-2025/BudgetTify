namespace Budgetify.Models;

public class ApiResponse<T>: BaseResponse
{
    public T Data { get; set; }
    public ApiResponse(T data)
    {
        Data = data;
    }
    public ApiResponse(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
    public ApiResponse(int errorCode, string errorMessage, T data)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
        Data = data;
    }
}