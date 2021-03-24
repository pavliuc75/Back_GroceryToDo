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
        [Route("{recordId:int}")]
        public async Task<ActionResult<Record>> GetRecordByIdAsync([FromRoute] int recordId)
        {
            try
            {
                Record record = await recordsService.GetRecordByIdAsync(recordId);
                return Ok(record);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<Record>> CreateRecordAsync()
        {
            try
            {
                Record record = await recordsService.CreateRecordAsync();
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

        [HttpPatch]
        public async Task<ActionResult> UpdateRecordDescriptionAsync([FromQuery] int recordId,
            [FromQuery] string description)
        {
            try
            {
                await recordsService.UpdateRecordDescriptionAsync(recordId, description);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteRecordAsync([FromQuery] int itemId, [FromQuery] int recordId)
        {
            try
            {
                await recordsService.RemoveItemFromRecordAsync(itemId, recordId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{recordId:int}")]
        public async Task<ActionResult> WipeRecordAsync([FromRoute] int recordId)
        {
            try
            {
                await recordsService.WipeRecordAsync(recordId);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}