using AspNetCoreODataWithModel.Data.Entities;
using AspNetCoreODataWithModel.Data.Repositories;
using AspNetCoreODataWithModel.Infrastructure.Helper;
using AspNetCoreODataWithModel.Model;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreODataWithModel.Controllers
{
    /// <summary>
    /// TaskController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ODataController
    {
        private readonly IMapper mapper;
        private readonly ITaskRepository repository;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="repository"></param>
        public TaskController(IMapper mapper, ITaskRepository repository)
        { 
            this.mapper = mapper;
            this.repository = repository;
        }

        /// <summary>
        /// Get all tasks using OData query
        /// </summary>
        /// <returns>tasks</returns>
        /// <remarks>
        /// This method uses OData 4.0 query to retrieve task history data. (http://docs.oasis-open.org/odata/odata/v4.01/odata-v4.01-part2-url-conventions.html/)
        /// 
        /// ### REMARKS ###
        /// Fields and their type you can filter data are the following:
        /// 
        /// - id:                 integer($int32) 
        /// - name:               string 
        /// - billable:           string 
        /// - observations:       string
        /// - date:               string ($date-time) 
        /// </remarks>
        /// <response code="200">Returns tasks</response>
        /// <response code="400">Error getting tasks</response>
        /// <response code="404">Object result not found</response>
        /// <response code="500">Internal Server Error</response> 
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<TaskModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BadRequestObjectResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(NotFoundObjectResult), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [EnableQuery(AllowedQueryOptions = AllowedQueryOptions.All)] 
        public IActionResult Get(ODataQueryOptions<TaskModel> filter)
        {
            var tasks = repository.ListAll();
            if (!string.IsNullOrWhiteSpace(filter.Filter?.RawValue))
            {
                IEdmModel model = EdmModelHelper.GetEdmModel();
                IEdmType type = model.FindDeclaredType("AspNetCoreODataWithModel.Data.Entities.Tarea");
                IEdmNavigationSource source = model.FindDeclaredEntitySet("Tareas");
                ODataQueryOptionParser parser = new ODataQueryOptionParser(model, type, source, new Dictionary<string, string> { { "$filter", filter.Filter?.RawValue } });
                ODataQueryContext context = new ODataQueryContext(model, typeof(Tarea), filter.Context.Path);
                FilterQueryOption newfilter = new FilterQueryOption(filter.Filter?.RawValue, context, parser);

                tasks = newfilter.ApplyTo(tasks, new ODataQuerySettings()) as IQueryable<Tarea>;
            }

            var results = tasks.Select(p => mapper.Map<TaskModel>(p));
            var page = new PageResult<TaskModel>(mapper.Map<IEnumerable<TaskModel>>(results.ToList()),
                Request.HttpContext.ODataFeature().NextLink,
                Request.HttpContext.ODataFeature().TotalCount);

            return Ok(page);
        }
    }
}
