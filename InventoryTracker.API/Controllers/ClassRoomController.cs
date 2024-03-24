using InventoryTracker.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassRoomController : ControllerBase
    {
        IClassRoomLogic _logic;

        public ClassRoomController(IClassRoomLogic logic)
        {
            _logic= logic;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllClassRooms()
        {
            try
            {
                return Ok(await _logic.GetAllClassRooms());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
