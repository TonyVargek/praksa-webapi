using AutoMapper;
using Example.Common;
using Example.Model;
using Example.Service.Common;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        protected IFoodService FoodService { get; }
        protected IMapper Mapper { get; }

        public FoodController(IFoodService foodService, IMapper mapper)
        {
            FoodService = foodService;
            Mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(string typeMeal = null, string brand = null)
        {
            FoodFilter filter = new FoodFilter()
            {
                TypeMeal = typeMeal,
                Brand = brand
            };
            var foods = await FoodService.GetAllAsync(filter);
            if (foods != null)
            {
                return Ok(foods);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var food = await FoodService.GetByIdAsync(id);
            if (food != null)
            {
                return Ok(food);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Food food)
        {
            var res = await FoodService.AddAsync(food);
            if (res == true)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPost("many")]
        public async Task<IActionResult> PostMany(List<Food> foods)
        {
            var res = await FoodService.AddAsync(foods);
            if (res == true)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, RestFood restFood)
        {
            var res = await FoodService.UpdateAsync(id, Mapper.Map<Food>(restFood));
            if (res == true)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await FoodService.DeleteAsync(id);
            if (res == true)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}