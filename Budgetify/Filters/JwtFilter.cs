using Budgetify.Enums;
using Budgetify.Models;
using Budgetify.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Budgetify.Services;

namespace Budgetify.Filters;

public class JwtFilter : ActionFilterAttribute
{
    private readonly IJwtService _jwtService;

    public JwtFilter(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var user = _jwtService.GetUserToken();

        if (user == null || user.UserId == 0)
        {
            context.Result = UnauthorizedResponse();
            return;
        }

        if (!context.HttpContext.Request.Headers.TryGetValue("Authorization", out var token))
        {
            context.Result = UnauthorizedResponse();
            return;
        }

        var validatedToken = _jwtService.ValidateToken(token.ToString().Replace("Bearer ", ""));
        if (validatedToken is null)
        {
            context.Result = UnauthorizedResponse();
            return;
        }

        await next();
    }

    private JsonResult UnauthorizedResponse()
    {
        return new JsonResult(new BaseResponse((int)EnumErrorCode.Unauthorized, "Unauthorized"))
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
    }
}