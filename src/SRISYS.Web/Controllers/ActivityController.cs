namespace Srisys.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Services.Interfaces;

    /// <summary>
    /// <see cref="ActivityController"/> class handles ActivityLog basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize]
    public class ActivityController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Activity, ActivitySummary> builder;
        private readonly IAdjustmentService adjustmentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivityController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <param name="mapper">Automapper</param>
        /// <param name="builder">Builder</param>
        /// <param name="adjustmentService">The adjustment service</param>
        public ActivityController(SrisysDbContext context, IMapper mapper, ISummaryListBuilder<Activity, ActivitySummary> builder, IAdjustmentService adjustmentService)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
            this.adjustmentService = adjustmentService;
        }

        /// <summary>
        /// Returns list of active <see cref="Activity"/>
        /// </summary>
        /// <param name="filter"><see cref="ActivityFilterRequest"/></param>
        /// <returns>List of Activity</returns>
        [HttpPost("search", Name = "GetAllActivities")]
        public async Task<IActionResult> GetAll([FromBody]ActivityFilterRequest filter)
        {
            var list = this.FetchAndFilterActivities(filter);
            var entities = await this.builder.BuildAsync(list, filter);

            return this.Ok(entities);
        }

        /// <summary>
        /// Returns a lookup of borrowed items.
        /// </summary>
        /// <param name="filter">The filter</param>
        /// <returns>A list of borrowed items</returns>
        [HttpPost("lookup", Name = "GetActivitiesLookup")]
        public async Task<IActionResult> GetLookup([FromBody]ActivityFilterRequest filter)
        {
            var list = await Task.Run(() => this.FetchAndFilterActivities(filter));
            var mappedList = this.mapper.Map<IEnumerable<ActivitySummary>>(list);

            return this.Ok(mappedList);
        }

        /// <summary>
        /// Creates an <see cref="Activity"/>.
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

            foreach (var activityToSave in entity.Activities)
            {
                var activity = activityToSave.Id == 0 ?
                    this.mapper.Map<Activity>(activityToSave) :
                    await this.context.Activities.FindAsync(activityToSave.Id);

                if (activityToSave.MaterialId > 0)
                {
                    var material = await this.context.Materials.Include(c => c.Type).SingleOrDefaultAsync(c => c.Id == activityToSave.MaterialId);

                    // set quantities
                    if (entity.Type == ActivityType.Borrow)
                    {
                        activity.QuantityBorrowed = activityToSave.Quantity;
                        if (material.Type != null && material.Type.Code == Constants.Consumable)
                        {
                            material.Quantity = material.Quantity - activityToSave.Quantity;
                        }

                        material.RemainingQuantity = material.RemainingQuantity - activityToSave.Quantity;
                    }
                    else if (entity.Type == ActivityType.Return)
                    {
                        activity.TotalQuantityReturned = activity.TotalQuantityReturned + activityToSave.Quantity;
                        if (material.Type != null && material.Type.Code == Constants.Consumable)
                        {
                            material.Quantity = material.Quantity + activityToSave.Quantity;
                        }

                        material.RemainingQuantity = material.RemainingQuantity + activityToSave.Quantity;
                        activity.LatestReturnDate = activityToSave.Date;
                    }

                    // set status
                    activity.Status = activity.QuantityBorrowed == activity.TotalQuantityReturned ? ActivityStatus.Complete : ActivityStatus.Pending;
                }

                if (activityToSave.Id == 0)
                {
                    await this.context.Activities.AddAsync(activity);
                }
                else
                {
                    activity.ReturnedById = activityToSave.ReturnedById;
                    activity.ReceivedById = activityToSave.ReceivedById;

                    this.context.Update(activity);
                }

                await this.context.SaveChangesAsync();
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

            if (activity.Status == ActivityStatus.Complete)
            {
                return this.BadRequest();
            }

            this.context.Remove(activity);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }

        // Private Methods
        private IQueryable<Activity> FetchAndFilterActivities(ActivityFilterRequest filter)
        {
            // get list of active activities (not deleted)
            var list = this.context.Activities
                .Include(c => c.Material)
                .AsNoTracking();

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

            if (!(filter?.BorrowedById).IsNullOrZero())
            {
                list = list.Where(c => c.BorrowedById == filter.BorrowedById);
            }

            if (!(filter?.ReleasedById).IsNullOrZero())
            {
                list = list.Where(c => c.ReleasedById == filter.ReleasedById);
            }

            if (!(filter?.MaterialId).IsNullOrZero())
            {
                list = list.Where(c => c.MaterialId == filter.MaterialId);
            }

            // sort
            var ordering = $"Date {Constants.SortDirectionDescending}";
            if (!string.IsNullOrEmpty(filter?.SortBy))
            {
                ordering = $"{filter.SortBy} {filter.SortDirection}";
            }

            list = list.OrderBy(ordering);

            return list;
        }
    }
}