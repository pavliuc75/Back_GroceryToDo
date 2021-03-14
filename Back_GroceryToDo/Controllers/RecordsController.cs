using System;
using System.Threading.Tasks;
using Back_GroceryToDo.Data;
using Back_GroceryToDo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Back_GroceryToDo.Controllers
{
    [ApiController]
    [Route("list")]
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
    }
}