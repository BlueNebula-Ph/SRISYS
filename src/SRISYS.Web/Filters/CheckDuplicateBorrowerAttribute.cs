namespace Srisys.Web.Filters
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.DTO;

    public class CheckDuplicateBorrowerAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        public CheckDuplicateBorrowerAttribute(SrisysDbContext dbContext)
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

            var model = input as SaveBorrowerRequest;
            if (model == null)
            {
                return;
            }

            var entity = this.dbContext.Borrowers.FirstOrDefault(c => c.Name == model.Name);
            if (entity != null)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new ContentResult() { Content = "Duplicate not allowed" };
                context.ModelState.AddModelError("400", "Borrower name already exists");
            }

            return;
        }
    }
}
