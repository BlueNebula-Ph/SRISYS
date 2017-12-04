namespace Srisys.Web.Filters
{
    using BlueNebula.Common.DTOs;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.Common;
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;

    public class InjectMetadataAttribute : ActionFilterAttribute
    {
        private readonly ActionType actionType;
        private readonly string propertyName;

        public InjectMetadataAttribute(ActionType actionType, string propertyName)
        {
            this.actionType = actionType;
            this.propertyName = propertyName;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var input = context.ActionArguments[this.propertyName];
            if (input == null)
            {
                return;
            }

            var model = input as SaveRequestBase;
            if (model == null)
            {
                return;
            }

            var sidValue = context.HttpContext.User.Claims.FirstOrDefault(a => a.Type == JwtRegisteredClaimNames.Sid)?.Value;
            if (string.IsNullOrEmpty(sidValue))
            {
                return;
            }

            if (int.TryParse(sidValue, out int userId))
            {
                if (this.actionType == ActionType.Create)
                {
                    model.CreatedBy = userId;
                    model.CreatedDate = DateTime.Now;
                }

                model.LastUpdatedBy = userId;
                model.LastUpdatedDate = DateTime.Now;
            }
        }
    }
}
