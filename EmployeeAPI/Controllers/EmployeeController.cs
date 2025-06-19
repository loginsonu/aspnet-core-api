using EmployeeAPI.Data;
using EmployeeAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _repository;

        public EmployeeController(EmployeeRepository repository)
        {
            _repository = repository;
        }

        // 🔹 GET: api/employee
        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            var employees = _repository.GetAllEmployees();
            return Ok(employees);
        }

        // 🔹 GET: api/employee/5
        [HttpGet("{id}")]
        public ActionResult<Employee> GetEmployee(int id)
        {
            var employee = _repository.GetEmployee(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        // 🔹 POST: api/employee
        [HttpPost]
        public ActionResult AddEmployee([FromBody] Employee employee)
        {
            _repository.AddEmployee(employee);
            return Ok(new { message = "Employee added successfully" });
        }

        // 🔹 PUT: api/employee/5
        [HttpPut("{id}")]
        public ActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            var existing = _repository.GetEmployee(id);
            if (existing == null)
            {
                return NotFound();
            }

            employee.Id = id; // ensure ID is set
            _repository.UpdateEmployee(employee);
            return Ok(new { message = "Employee updated successfully" });
        }

        // 🔹 DELETE: api/employee/5
        [HttpDelete("{id}")]
        public ActionResult DeleteEmployee(int id)
        {
            var existing = _repository.GetEmployee(id);
            if (existing == null)
            {
                return NotFound();
            }

            _repository.DeleteEmployee(id);
            return Ok(new { message = "Employee deleted successfully" });
        }
    }
}
