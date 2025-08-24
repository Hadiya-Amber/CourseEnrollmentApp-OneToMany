using Microsoft.AspNetCore.Mvc;
using Restuarant_Management.Interfaces;

namespace Restuarant_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiDemoController : ControllerBase
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
                Transient = new[] { _t1.Id, _t2.Id },
                Scoped = new[] { _s1.Id, _s2.Id },
                Singleton = new[] { _sg1.Id, _sg2.Id }
            });
        }
    }

}
