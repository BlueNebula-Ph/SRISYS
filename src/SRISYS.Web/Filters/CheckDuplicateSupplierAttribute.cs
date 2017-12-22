namespace Srisys.Web.Filters
{
    using System.Linq;
    using BlueNebula.Common.DTOs;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Srisys.Web.DTO;

    public class CheckDuplicateSupplierAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        public CheckDuplicateSupplierAttribute(SrisysDbContext dbContext)
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

            var model = input as SaveSupplierRequest;
            if (model == null)
            {
                return;
            }

            var entity = this.dbContext.Suppliers.FirstOrDefault(c => c.Name == model.Name);
            if (entity != null)
            {
                context.HttpContext.Response.StatusCode = 400;
                context.Result = new ContentResult() { Content = "Duplicate not allowed" };
                context.ModelState.AddModelError("400", "Supplier name already exists");
            }

            return;
        }
    }
}
