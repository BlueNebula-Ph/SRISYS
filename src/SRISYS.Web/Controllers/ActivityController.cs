namespace Srisys.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;

    /// <summary>
    /// <see cref="ActivityController"/> class handles ActivityLog basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ActivityController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Activity, ActivitySummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        public ActivityController(SrisysDbContext context, IMapper mapper, ISummaryListBuilder<Activity, ActivitySummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Activity"/>
        /// </summary>
        /// <param name="filter"><see cref="ActivityFilterRequest"/></param>
        /// <returns>List of Activity</returns>
        [HttpPost("search", Name = "GetAllActivities")]
        public async Task<IActionResult> GetAll([FromBody]ActivityFilterRequest filter)
        {
            // get list of active activities (not deleted)
            var list = this.context.Activities
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (filter?.ActivityStatus != null)
            {
                list = list.Where(c => c.Status == filter.ActivityStatus);
            }

            if (filter?.DateFrom != null || filter?.DateTo != null)
            {
                filter.DateFrom = filter.DateFrom == null || filter.DateFrom == DateTime.MinValue ? DateTime.Today : filter.DateFrom;
                filter.DateTo = filter.DateTo == null || filter.DateTo == DateTime.MinValue ? DateTime.Today : filter.DateTo;
                list = list.Where(c => c.Date >= filter.DateFrom && c.Date < filter.DateTo.Value.AddDays(1));
            }

            if (!string.IsNullOrEmpty(filter?.BorrowedBy))
            {
                list = list.Where(c => c.BorrowedBy.ToLower().Contains(filter.BorrowedBy.ToLower()));
            }

            if (!string.IsNullOrEmpty(filter?.ReleasedBy))
            {
                list = list.Where(c => c.ReleasedBy.ToLower().Contains(filter.ReleasedBy.ToLower()));
            }

            if (!(filter?.MaterialId).IsNullOrZero())
            {
                list = list.Where(c => c.ActivityDetails.Any(d => d.MaterialId == filter.MaterialId));
            }

            // sort
            var ordering = $"Name {Constants.DefaultSortDirection}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="Activity"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Activity</returns>
        [HttpGet("{id}", Name = "GetActivity")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Activities
                .AsNoTracking()
                .Include(c => c.ActivityDetails)
                    .ThenInclude(d => d.Material)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<ActivitySummary>(entity);

            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Activity"/>.
        /// </summary>
        /// <param name="entity">entity to be created</param>
        /// <returns>Activity</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveActivityRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var activity = this.mapper.Map<Activity>(entity);
            await this.context.Activities.AddAsync(activity);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetActivity", new { id = activity.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="Activity"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SaveActivityRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var activity = await this.context.Activities.SingleOrDefaultAsync(t => t.Id == id);
            if (activity == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, activity);
                this.context.Update(activity);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Activity"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var activity = await this.context.Activities.SingleOrDefaultAsync(t => t.Id == id);
            if (activity == null)
            {
                return this.NotFound(id);
            }

            activity.IsDeleted = true;
            this.context.Update(activity);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}