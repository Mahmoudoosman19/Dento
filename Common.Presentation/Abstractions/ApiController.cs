using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Common.Presentation.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender;

    protected IActionResult HandleResult(object result)
    {
        var resultType = result.GetType();
        var isSuccessProperty = resultType.GetProperty("IsSuccess");
        var isSuccess = (bool)isSuccessProperty!.GetValue(result)!;
        var messageProperty = resultType.GetProperty("Message");
        var message = (string)messageProperty!.GetValue(result)!;

        if (isSuccess)
            return base.Ok(result);
        if (!isSuccess
            && (message.Contains("not found") || message.Contains("غير موجود")))
            return base.NotFound(result);
        else
            return base.BadRequest(result);
    }

}
