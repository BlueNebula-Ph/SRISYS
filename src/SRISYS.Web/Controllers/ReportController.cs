namespace Srisys.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BlueNebula.Common.Helpers;
    using LinqKit;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Models;

    /// <summary>
    /// <see cref="ReportController"/> class handles processing reports.
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
        /// <returns>Returns a list of activities.</returns>
        [HttpPost("daily-activity")]
        public async Task<IActionResult> GetDailyActivityReport([FromBody]ActivityReportFilterRequest filter)
        {
            // Get list of active activities (not deleted)
            var list = this.context.Activities
                .AsNoTracking();

            var reportDate = filter?.ReportDate ?? DateTime.Now;
            var nextDate = reportDate.AddDays(1);

            // To build the predicate
            var predicate = PredicateBuilder.New<Activity>(false);

            // Filter
            predicate = predicate.And(c => c.Date >= reportDate && c.Date < nextDate);

            if (!(filter?.MaterialTypeId).IsNullOrZero())
            {
                predicate = predicate.And(c => c.Material.TypeId == filter.MaterialTypeId);

                // If material type is tool, add in unreturned materials
                if (filter.MaterialTypeId == (int)MaterialType.Tool)
                {
                    predicate.Or(c => c.Date < nextDate && c.Status != ActivityStatus.Complete && c.Material.TypeId == filter.MaterialTypeId);
                }
            }

            list = list.Where(predicate);

            // Sort by date, then by borrower then by item
            list = list.OrderByDescending(c => c.Date)
                .ThenBy(c => c.BorrowedBy)
                .ThenBy(c => c.Material.Name);

            var mappedList = await list.ProjectTo<ActivitySummary>().ToListAsync();

            return this.Ok(mappedList);
        }

        /// <summary>
        /// Returns a list of materials low in quantity.
        /// </summary>
        /// <returns>List of materials low in quantity</returns>
        [HttpGet("low-stock")]
        public async Task<IActionResult> GetLowStockReport()
        {
            // Get a list of materials.
            // Filter to get the ones low in quantity.
            // Only applicable to cosumables.
            var list = this.context.Materials
                .AsNoTracking()
                .Where(c => !c.IsDeleted &&
                    c.TypeId == (int)MaterialType.Consumable &&
                    c.Quantity < c.MinimumStock)
                .OrderBy(c => c.Name);

            var mappedList = await list.ProjectTo<MaterialSummary>().ToListAsync();

            return this.Ok(mappedList);
        }

        /// <summary>
        /// Returns a list of materials for reporting.
        /// </summary>
        /// <returns>List of materials.</returns>
        [HttpGet("stocks")]
        public async Task<IActionResult> GetStockList()
        {
            // Get a list of materials.
            var list = this.context.Materials
                .AsNoTracking()
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Name);

            var mappedList = await list.ProjectTo<MaterialSummary>().ToListAsync();

            return this.Ok(mappedList);
        }
    }
}