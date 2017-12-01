namespace Srisys.Web.Filters
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.Common;

    public class AuthorizePermissionFilter : IAsyncActionFilter
    {
        private readonly PermissionItem permissionItem;
        private readonly PermissionAction permissionAction;

        public AuthorizePermissionFilter(PermissionItem item, PermissionAction action)
        {
            this.permissionItem = item;
            this.permissionAction = action;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            bool isAuthorized = context.HttpContext.User
                .HasClaim(a => a.Type == permissionItem.ToString() && a.Value == this.permissionAction.ToString());

            if (!isAuthorized)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                await next();
            }
        }
    }
}
