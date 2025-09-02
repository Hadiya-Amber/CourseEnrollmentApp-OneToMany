using ApiAssessment.Interface;
using ApiAssessment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiAssessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployee _service;
        public EmployeeController(IEmployee service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmp() {
            var res = await _service.GetAllEmp();
            return Ok(res);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(int id)
        {
            var res=await _service.GetEmpById(id);
            return Ok(res);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmp(Employee emp)
        {
            var res= await _service.AddEmp(emp);
            return Ok(res);
        }

        [HttpPut("id")]
        public async Task<ActionResult> UpdateEmp(int id, Employee emp)
        {
            var res=await _service.UpdateEmp(id, emp);
            return Ok(res);
        }
        [HttpDelete("id")]
        public async Task<bool> DeleteEmp(int id)
        {
            var res=await _service.DeleteEmp(id);
            return res;
        }

    }
}
