using System;
using System.Threading.Tasks;
using Back_GroceryToDo.Data;
using Back_GroceryToDo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_GroceryToDo.Controllers
{
    [ApiController]
    [Route("Record")]
    public class RecordsController : ControllerBase
    {
        private IRecordsService recordsService;

        public RecordsController(IRecordsService recordsService)
        {
            this.recordsService = recordsService;
        }

        [HttpGet]
        public async Task<ActionResult<Record>> GetRecordByIdAsync(int id)
        {
            try
            {
                Record record = await recordsService.GetRecordByIdAsync(id);
                return Ok(record);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Route("{recordId:int}")]
        public async Task<ActionResult<Item>> AddItemToRecordAsync([FromBody] Item item, [FromRoute] int recordId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Item added = await recordsService.AddItemToRecordAsync(item, recordId);
                return Created($"/{added.Id}", added);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPatch]
        [Route("{recordId:int}")]
        public async Task<ActionResult<Item>> UpdateItemInRecordAsync([FromBody] Item item, [FromRoute] int recordId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Item updated = await recordsService.UpdateItemInRecordAsync(item, recordId);
                return Ok(updated);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}