using Example.Common;
using Example.Model;
using Example.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Text;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] FoodFilter filter)
        {
            FoodService service = new FoodService();
            var foods = await service.GetAllAsync(filter);
            if (foods != null)
            {
                return Ok(foods);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            FoodService service = new FoodService();
            var food = await service.GetByIdAsync(id);
            if (food != null)
            {
                return Ok(food);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Food food)
        {
            FoodService service = new FoodService();
            var res = await service.AddAsync(food);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost("many")]
        public async Task<IActionResult> PostMany(List<Food> foods)
        {
            FoodService service = new FoodService();
            var res = await service.AddAsync(foods);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Food newFood)
        {
            FoodService service = new FoodService();
            var res = await service.UpdateAsync(id, newFood);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            FoodService service = new FoodService();
            var res = await service.DeleteAsync(id);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
