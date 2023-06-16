using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly EmployeeDbContext _employeeDbContext;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(ILogger<EmployeeController> logger, EmployeeDbContext employeeDbContext)
        {
            _logger = logger;
            _employeeDbContext = employeeDbContext;
        }
        
        [HttpGet, Route("{id:int}")]
        public async Task<ActionResult<EmployeeDetail>> GetEmployeeById(int id)
        {
            _logger.LogInformation("get employee by ID");
            if (_employeeDbContext.EmployeeDetails == null)
            {
                return NotFound();
            }
            var employee = await _employeeDbContext.EmployeeDetails.FindAsync(id);
            return employee;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDetail>>> GetEmployeesList()
        {
            _logger.LogInformation("get employee all employees");
            if (_employeeDbContext.EmployeeDetails == null)
            {
                return NotFound();
            }

            return await _employeeDbContext.EmployeeDetails.ToListAsync();
        }




        [HttpPost]
        public async Task<ActionResult<EmployeeDetail>> CreateEmployee(EmployeeDetail employee)
        {
            _logger.LogInformation("add employees");

            _employeeDbContext.EmployeeDetails.Add(employee);
            await _employeeDbContext.SaveChangesAsync();

            return Ok();
        }  



        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(int id, EmployeeDetail employee)
        {
            _logger.LogInformation("update employee");
            if (id != employee.EmployeeId)
            {
                return BadRequest();
            }
             _employeeDbContext.Entry(employee).State = EntityState.Modified;
            try
            {
                await _employeeDbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                
            }
            return Ok();

        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            _logger.LogInformation("delete employee by ID");
            if (_employeeDbContext.EmployeeDetails == null)
            {
                return BadRequest();
            }
            var emp = await _employeeDbContext.EmployeeDetails.FindAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            _employeeDbContext.EmployeeDetails.Remove(emp);
            await _employeeDbContext.SaveChangesAsync();

            return Ok();
        }
            
        
    }
}

