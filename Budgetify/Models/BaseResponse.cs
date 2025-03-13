using Budgetify.Enums;

namespace Budgetify.Models;

public class BaseResponse
{
    public int ErrorCode { get; set; } = (int)EnumErrorCode.Success;
    public string ErrorMessage { get; set; } = "success";
    
    public BaseResponse(){}

    public BaseResponse(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
}