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
                var temp=_mapper.Map<Borrow>(borrow);
                await _logic.CreateBorrow(temp);
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
        [HttpPut("Return")]
        public async Task<IActionResult> ReturnBorrow(ReturnBorrowDTO borrow)
        {
            try
            {
                var temp = _mapper.Map<Borrow>(borrow);
                await _logic.ReturnBorrow(temp);
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
        [HttpGet("Employee")]
        public async Task<IActionResult>GetBorrowsByEmployeeId(Guid id,bool active)
        {
            try
            {
                var result = await _logic.GetBorrowsByEmployee(id, active);
                return Ok(result.Select(x=>x.ToDisplayDTO()));
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
                var result = await _logic.GetBorrowsByClassRoom(id, active);
                return Ok(result.Select(x => x.ToDisplayDTO()));
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
