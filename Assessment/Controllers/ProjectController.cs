using ApiAssessment.Interface;
using ApiAssessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProject _service;
        public ProjectController(IProject service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var res = await _service.GetAllProj();
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult> AddProject(Project project)
        {
            var res = await _service.AddProject(project);
            return Ok(res);
        }
    }
}
