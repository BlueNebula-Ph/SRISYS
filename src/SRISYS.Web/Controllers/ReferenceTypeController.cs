namespace Srisys.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Srisys.Web.DTO;
    using Srisys.Web.Models;

    /// <summary>
    /// <see cref="ReferenceTypeController"/> class handles ReferenceType basic add, edit, delete and get.
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReferenceTypeController : Controller
    {
        private readonly SrisysDbContext context;
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceTypeController"/> class.
        /// </summary>
        /// <param name="context">DbContext</param>
        /// /// <param name="mapper">Automapper</param>
        public ReferenceTypeController(SrisysDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns list of active <see cref="ReferenceType"/>
        /// </summary>
        /// <returns>List of ReferenceType</returns>
        [HttpGet(Name = "GetAllReferenceTypes")]
        public async Task<IActionResult> GetAll()
        {
            // get list of active referenceTypes (not deleted)
            var list = await this.context.ReferenceTypes.Where(c => !c.IsDeleted).ToListAsync();

            // filter

            // sort

            // paging
            var entities = this.mapper.Map<IList<ReferenceTypeSummary>>(list);

            return this.Ok(entities);
        }

        /// <summary>
        /// Gets a specific <see cref="ReferenceType"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>ReferenceType</returns>
        [HttpGet("{id}", Name = "GetReferenceType")]
        public async Task<IActionResult> GetById(long id)
        {
            var entity = await this.context.ReferenceTypes.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return this.NotFound(id);
            }

            return this.Ok(this.mapper.Map<ReferenceTypeSummary>(entity));
        }

        /// <summary>
        /// Creates a <see cref="ReferenceType"/>.
        /// </summary>
        /// <param name="request">entity</param>
        /// <returns>ReferenceType</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveReferenceTypeRequest request)
        {
            if (request == null || !this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            var entity = this.mapper.Map<ReferenceType>(request);
            await this.context.ReferenceTypes.AddAsync(entity);
            await this.context.SaveChangesAsync();

            return this.CreatedAtRoute("GetReferenceType", new { id = entity.Id }, entity);
        }

        /// <summary>
        /// Updates a specific <see cref="ReferenceType"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="request">entity</param>
        /// <returns>None</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] SaveReferenceTypeRequest request)
        {
            if (request == null || request.Id == 0 || id == 0)
            {
                return this.BadRequest();
            }

            var entity = await this.context.ReferenceTypes.SingleOrDefaultAsync(t => t.Id == id);
            if (entity == null)
            {
                return this.NotFound(id);
            }

            try
            {
                this.mapper.Map(request, entity);
                this.context.Update(entity);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return this.BadRequest(ex);
            }

            return new NoContentResult();
        }

        /// <summary>
        /// Deletes a specific <see cref="ReferenceType"/>.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>None</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var entity = await this.context.ReferenceTypes.FirstAsync(t => t.Id == id);
            if (entity == null)
            {
                return this.NotFound(id);
            }

            entity.IsDeleted = true;
            this.context.Update(entity);
            await this.context.SaveChangesAsync();

            return new NoContentResult();
        }
    }
}