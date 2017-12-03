namespace Srisys.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using AutoMapper;
    using BlueNebula.Common.Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Srisys.Web.Common;
    using Srisys.Web.DTO;
    using Srisys.Web.Services.Interfaces;

    /// <summary>
    /// <see cref="CategoryController"/> class handles ActivityLog basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;
        private readonly ISummaryListBuilder<Category, CategorySummary> builder;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryController"/> class.
        /// </summary>
        /// <param name="context">The DbContext</param>
        /// <param name="mapper">The mapper</param>
        /// <param name="builder">The summary list builder</param>
        public CategoryController(SrisysDbContext context, IMapper mapper, ISummaryListBuilder<Category, CategorySummary> builder)
        {
            this.context = context;
            this.mapper = mapper;
            this.builder = builder;
        }

        /// <summary>
        /// Returns list of active <see cref="Category"/>
        /// </summary>
        /// <param name="filter"><see cref="CategoryFilterRequest"/></param>
        /// <returns>List of Categorys</returns>
        [HttpPost("search", Name = "GetAllCategorys")]
        public async Task<IActionResult> GetAll([FromBody]CategoryFilterRequest filter)
        {
            // get list of active categorys (not deleted)
            var list = this.context.Categories
                .AsNoTracking()
                .Where(c => !c.IsDeleted);

            // filter
            if (!string.IsNullOrEmpty(filter?.SearchTerm))
            {
                list = list.Where(c => c.Name.ToLower().Contains(filter.SearchTerm.ToLower()));
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
        /// Gets a specific <see cref="Category"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Category summary object</returns>
        [HttpGet("{id}", Name = "GetCategory")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.Categories
                .AsNoTracking()
                .Include(c => c.Subcategories)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (entity == null)
            {
                return this.NotFound(id);
            }

            var mappedEntity = this.mapper.Map<CategorySummary>(entity);
            return this.Ok(mappedEntity);
        }

        /// <summary>
        /// Creates a <see cref="Category"/>.
        /// </summary>
        /// <param name="entity">Category to be created</param>
        /// <returns>Category object</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]SaveCategoryRequest entity)
        {
            if (entity == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var category = this.mapper.Map<Category>(entity);
            await this.context.Categories.AddAsync(category);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetCategory", new { id = category.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="Category"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="entity">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody]SaveCategoryRequest entity)
        {
            if (entity == null || entity.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var category = await this.context.Categories
                .AsNoTracking()
                .SingleOrDefaultAsync(t => t.Id == id);

            if (category == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(entity, category);
                this.context.Update(category);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="Category"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var category = await this.context.Categories.SingleOrDefaultAsync(t => t.Id == id);
            if (category == null)
            {
                return this.NotFound(id);
            }

            category.IsDeleted = true;
            this.context.Update(category);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}
