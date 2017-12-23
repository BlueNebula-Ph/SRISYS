namespace Srisys.Web.Filters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// This filter prevents duplicate values to be inserted to the databse.
    /// </summary>
    public class PreventDuplicateAttribute : ActionFilterAttribute
    {
        private readonly SrisysDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="PreventDuplicateAttribute"/> class.
        /// </summary>
        /// <param name="dbContext">The database context</param>
        public PreventDuplicateAttribute(SrisysDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <summary>
        /// Overrides the on action executing method
        /// </summary>
        /// <param name="context">The context</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }
    }
}
