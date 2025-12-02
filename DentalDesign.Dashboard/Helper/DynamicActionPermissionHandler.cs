using Microsoft.AspNetCore.Authorization;

namespace DentalDesign.Dashboard.Helper
{
    public class DynamicActionPermissionRequirement : IAuthorizationRequirement { }

    public class DynamicActionPermissionHandler : AuthorizationHandler<DynamicActionPermissionRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DynamicActionPermissionRequirement requirement)
            {
            // 1. خذ الصلاحية المطلوبة من(HttpContext.Items)
            if (context.Resource is HttpContext httpContext)
            {
                var requiredPermission = httpContext.Items["RequiredPermission"] as string;

                if (string.IsNullOrEmpty(requiredPermission))
                {
                    context.Fail();
                    return Task.CompletedTask;
                }

                if (httpContext.User.HasClaim(c => c.Type == "Permissions" && c.Value == "Admin"))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }

                if (httpContext.User.HasClaim("Permissions", requiredPermission))
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }

            context.Fail();
            return Task.CompletedTask;
        }
    }
}
