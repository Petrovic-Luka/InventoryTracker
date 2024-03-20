﻿using AutoMapper;
using InventoryTracker.BusinessLogic.Interfaces;
using InventoryTracker.Domain;
using InventoryTrackerDTO.Equipment;
using Microsoft.AspNetCore.Mvc;

namespace InventoryTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EquipmentController : ControllerBase
    {
        IEquipmentLogic _logic;
        IMapper mapper;
        public EquipmentController(IEquipmentLogic logic, IMapper mapper)
        {
            _logic = logic;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment(CreateEquipmentDTO equipment)
        {
            try
            {
                var temp = mapper.Map<Equipment>(equipment);
                await _logic.CreateEquipment(temp);
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

        [HttpGet("type")]
        public async Task<IActionResult> GetEquipmentByType(int typeId,bool available)
        {
            try
            {

                return Ok(await _logic.GetEquipmentByType(typeId,available));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error has occurred");
            }
        }
    }
}
