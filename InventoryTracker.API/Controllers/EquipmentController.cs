using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.Domain;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : ControllerBase
    {
        IEquipmentLogic _logic;

        public EquipmentController(IEquipmentLogic logic)
        {
            _logic = logic;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment(Equipment equipment)
        {
            try
            {
                await _logic.CreateEquipment(equipment);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error has occurred");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEquipment()
        {
            try
            {

                return Ok(await _logic.GetAllEquipment());
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error has occurred");
            }
        }
    }
}
