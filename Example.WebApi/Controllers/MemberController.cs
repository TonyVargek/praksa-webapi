using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private static IList<Member> _members = new List<Member>();
        public MemberController()
        {
        }

        [HttpGet("getAll")]
        public IActionResult GetAll(string? firstName = null, string? lastName = null, string? favoriteFood = null, float bmi = -1)
        {
            var filter = _members.AsEnumerable();
            if(firstName != null)
               filter = filter.Where(x => x.FirstName == firstName);
            if (lastName != null)
                filter =filter.Where(x => x.LastName == lastName);
            if (favoriteFood != null)
                filter = filter.Where(x => x.FavoriteFood?.Name == favoriteFood);
            if (bmi > 0)
                filter = filter.Where(x => x.BMI > bmi);
            filter.ToList();
            
            if (filter.Count() > 0)
                return Ok(filter);
            return NotFound("There are no records");
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var member = _members.FirstOrDefault(x => x.Id == id);
            if (member == null)
                return NotFound("Member does not exists");
            return Ok(member);
        }

        [HttpPost]
        public IActionResult Post(Member member)
        {
            if (_members.Count() > 0)
                member.Id = _members.Max(x => x.Id) + 1;
            else
                member.Id = 1;
            var count = _members.Count();
            _members.Add(member);
            if (_members.Count() - count > 0)
                return Ok("Member added succesfully");
            return BadRequest("Something went wrong");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Member newMember)
        {
            newMember.Id = id;
            var member = _members.FirstOrDefault(x => x.Id == id);
            if (member == null)
            {
                return NotFound("Member does not exist");
            }
            member.FirstName = newMember.FirstName;
            member.LastName = newMember.LastName;
            member.Id = newMember.Id;
            member.FavoriteFood = newMember.FavoriteFood;
            member.Weight = newMember.Weight;
            member.Height = newMember.Height;
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var member = _members.FirstOrDefault(x => x.Id == id);
            if (member == null)
                return NotFound("");
            _members.Remove(member);
            return NoContent();
        }

        [HttpGet("getfoodbybmi")]
        public IActionResult GetFoodByBMI()
        {
            var filter = _members.AsEnumerable();

            if (!filter.Any())
                return NotFound("There are no Members");

            filter = filter.Where(x => x.BMI > 20 && x.BMI <= 35);
            filter = filter.Where(x => x.FavoriteFood?.TypeMeal == "Snack" || x.FavoriteFood?.TypeMeal == "Desert");
            filter = filter.Where(x => x.FavoriteFood?.Brand == "Lidl");
            var filterFood = filter.Select(x => x.FavoriteFood).ToList();

            if (filterFood.Any())
                return Ok(filterFood);
            return NotFound("");
        }

    }
}
