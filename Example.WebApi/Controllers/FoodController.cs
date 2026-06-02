using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private static List<Food> _foods = new List<Food>();
        public FoodController()
        {

        }

        [HttpGet("getAll")]
        public IActionResult getAll([FromQuery] string typeMeal, string brand)
        {
            var filter = _foods.AsEnumerable();
            if (typeMeal != null)
                filter = filter.Where(x => x.TypeMeal == typeMeal);
            if (brand != null)
                filter = filter.Where(x => x.Brand == brand);
            filter = filter.ToList();
            if (filter.Count() > 0)
                return Ok(filter);
            return BadRequest("There are no records");
        }
  
        [HttpGet("{id}")]
        public IActionResult FindFood(int id)
        {
            var food = _foods.FirstOrDefault(x => x.Id == id);
            if (food != null)
                return Ok(food);
            return NotFound("Food is not found");
        }

        [HttpPost]
        public IActionResult AddFood(Food food)
        {
            if (_foods.Count() > 0)
                food.Id = _foods.Max(x => x.Id) + 1;
            else
                food.Id = 1;
            var count = _foods.Count();
            _foods.Add(food);
            if (_foods.Count() - count > 0)
                return Ok(food);
            return BadRequest("Something went wrong");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateFood(int id, Food newFood)
        {
            var idx = _foods.FindIndex(x => x.Id == id);
            if (idx == -1)
                return NotFound("Food does not exist");
            _foods[idx] = newFood;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveFood(int id)
        {
            var food = _foods.FirstOrDefault(x => x.Id == id);
            if (food == null)
                return NotFound("Food does not exist");
            _foods.Remove(food);
            return NoContent();
        }
    }
}
