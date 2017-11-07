namespace Srisys.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Srisys.Web.DTO;

    /// <summary>
    /// <see cref="ReportController"/> class handles Activity reports.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        public ReportController(SrisysDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns a list of borrowed/returned items.
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>Returns a list of borrowed/returned items.</returns>
        [HttpPost("daily-activity")]
        public async Task<IActionResult> GetDailyActivityReport([FromBody]ActivityReportFilterRequest filter)
        {
            // get list of active activities (not deleted)
            var list = this.context.Activities
                .Include(c => c.Material)
                .AsNoTracking();

            // filter
            if (filter?.ReportDate != null)
            {
                list = list.Where(c => c.Date >= filter.ReportDate && c.Date < filter.ReportDate.Value.AddDays(1));
            }

            if (!(filter?.MaterialTypeId).IsNullOrZero())
            {
                list = list.Where(c => c.Material.TypeId == filter.MaterialTypeId);
            }

            var mappedList = this.mapper.Map<IEnumerable<ActivitySummary>>(list);

            return this.Ok(mappedList);
        }
    }
}