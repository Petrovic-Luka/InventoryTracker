using AutoMapper;
using InventoryTracker.API.Mappers;
using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.DataAccess.Interfaces;
using InventoryTracker.Domain;
using InventoryTrackerDTO;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        IMapper mapper;
        IEmployeeLogic logic;
        public EmployeeController(IMapper mapper, IEmployeeLogic logic)
        {
            this.logic = logic;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(EmployeeDTO employeeDTO)
        {
            try
            {
                var temp = mapper.Map<Employee>(employeeDTO);
                await logic.CreateEmployeeAsync(temp);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }     
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            try
            {
                return Ok(await logic.GetAllEmployeesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            try
            {
                await logic.DeleteEmployeeAsync(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeDTO employeeDTO)
        {
            try
            {
                var temp = mapper.Map<Employee>(employeeDTO);
                await logic.UpdateEmployeeAsync(temp);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
