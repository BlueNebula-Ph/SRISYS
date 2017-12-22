namespace Srisys.Web.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.DTO;

    public class CheckDuplicateUserAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        public CheckDuplicateUserAttribute(SrisysDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var input = context.ActionArguments["entity"];
            if (input == null)
            {
                return;
            }

            var model = input as SaveUserRequest;
            if (model == null)
            {
                return;
            }

            var entity = this.dbContext.Users.FirstOrDefault(c => c.Username == model.Username && c.Id != model.Id);
            if (entity != null)
            {
                context.ModelState.AddModelError("400", "Usernname already exists");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            return;
        }
    }
}
