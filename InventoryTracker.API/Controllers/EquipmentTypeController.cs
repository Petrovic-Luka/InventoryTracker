using AutoMapper;
using InventoryTracker.API.Mappers;
using InventoryTracker.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentTypeController : Controller
    {

        IEquipmentTypeLogic _logic;
        IMapper _mapper;

        public EquipmentTypeController(IEquipmentTypeLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetEquipmentTypes()
        {
            try
            {
                return Ok((await _logic.GetEquipmentTypes()).Select(x=>x.ToEquipmentTypeDTO()));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
