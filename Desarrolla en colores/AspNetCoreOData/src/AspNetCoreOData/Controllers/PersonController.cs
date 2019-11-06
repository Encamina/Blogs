using AspNetCoreOData.Database;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspNetCoreOData.Controllers
{
    [ODataRoutePrefix("Person")]
    public class PersonController : ODataController
    {
        private AdventureWorks2016Context db;

        public PersonController(AdventureWorks2016Context adventureWorks2016Context)
        {
            db = adventureWorks2016Context;
        }

        [ODataRoute]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(db.Person.AsQueryable());
        }

        [ODataRoute("({key})")]
        [EnableQuery(PageSize = 20, AllowedQueryOptions = AllowedQueryOptions.All)]
        [HttpGet]
        public IActionResult Get([FromODataUri] int key)
        {
            return Ok(db.Person.Find(key));
        } 
    }
}