﻿namespace Srisys.Web.Filters
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

            var entity = this.dbContext.Borrowers.FirstOrDefault(c => c.Name == model.Name && c.Id != model.Id);
            if (entity != null)
            {
                context.ModelState.AddModelError("400", "Borrower name already exists");
                context.Result = new BadRequestObjectResult(context.ModelState);
            }

            return;
        }
    }
}
