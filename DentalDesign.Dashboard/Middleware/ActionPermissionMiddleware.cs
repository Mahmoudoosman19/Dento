using Microsoft.AspNetCore.Mvc.Controllers;

namespace DentalDesign.Dashboard.Middleware
{
    public class ActionPermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public ActionPermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            if (endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>() is { } actionDescriptor)
            {
                var controllerName = actionDescriptor.ControllerName;
                var actionName = actionDescriptor.ActionName;
                var permissionName = $"{controllerName}.{actionName}";

                context.Items["RequiredPermission"] = permissionName;
            }

            await _next(context);
        }
    }
}
