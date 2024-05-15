using AutoMapper;
using InventoryTracker.API.Mappers;
using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.Domain;
using InventoryTrackerDTO.Borrow;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BorrowController : ControllerBase
    {
        IBorrowLogic _logic;
        IMapper _mapper;

        public BorrowController(IBorrowLogic logic, IMapper mapper)
        {
            _logic = logic;
            _mapper=mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBorrow(CreateBorrowDTO borrow)
        {
            try
            {
                await _logic.CreateBorrow(borrow);
                return Ok("Borrow saved");
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
        [HttpPut("Return")]
        public async Task<IActionResult> ReturnBorrow(ReturnBorrowDTO borrow)
        {
            try
            {
                await _logic.ReturnBorrow(borrow);
                return Ok("Return saved");
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
        [HttpGet("Employee")]
        public async Task<IActionResult>GetBorrowsByEmployeeId(string email,bool active)
        {
            try
            {
                var result = await _logic.GetBorrowsByEmployee(email, active);
                return Ok(result.Select(x=>x.ToEmployeeDisplayDTO()));
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
        [HttpGet("ClassRoom")]
        public async Task<IActionResult> GetBorrowsByClassRoomId(Guid id, bool active)
        {
            try
            {
                return Ok((await _logic.GetBorrowsByClassRoom(id, active)).Select(x => x.ToDisplayDTO()));
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

        [HttpGet("Equipment")]
        public async Task<IActionResult> GetBorrowsByEquipmentId(Guid id, bool active)
        {
            try
            {
                var result = await _logic.GetBorrowsByEquipment(id, active);
                return Ok(result.Select(x => x.ToHistoryDisplayDTO()));
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
