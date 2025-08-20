using ManyToMany.Services;
using Microsoft.AspNetCore.Mvc;

namespace ManyToMany.Controllers
{
    [ApiController]   // 👈 tells ASP.NET Core this is a Web API controller
    [Route("api/[controller]")] // 👈 endpoint will be /api/DiDemo
    public class DiDemoController : ControllerBase   // 👈 use ControllerBase
    {
        private readonly IOperationTransient _t1, _t2;
        private readonly IOperationScoped _s1, _s2;
        private readonly IOperationSingleton _sg1, _sg2;

        public DiDemoController(
            IOperationTransient t1, IOperationTransient t2,
            IOperationScoped s1, IOperationScoped s2,
            IOperationSingleton sg1, IOperationSingleton sg2)
        {
            _t1 = t1; _t2 = t2;
            _s1 = s1; _s2 = s2;
            _sg1 = sg1; _sg2 = sg2;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                Transient = new[] { _t1.Id, _t2.Id },   // different within same request
                Scoped = new[] { _s1.Id, _s2.Id },   // same within same request
                Singleton = new[] { _sg1.Id, _sg2.Id }  // same across all requests
            });
        }
    }
}
