using ApiAssessment.Interface;
using ApiAssessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartment _service;
        public DepartmentController(IDepartment service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllDept()
        {
            var res = await _service.GetAllDept();
            return Ok(res);
        }
        [HttpPost]
        public async Task<ActionResult> AddDept(Department dept)
        {
            var res = await _service.AddDept(dept);
            return Ok(res);
        }
        [HttpPut("id")]
        public async Task<ActionResult> UpdateDept(int id, Department dept)
        {
            var res = await _service.UpdateDept(id, dept);
            return Ok(res);
        }
    }
}
