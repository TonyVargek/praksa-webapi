using Example.Common;
using Example.Model;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Example.Service.Common;

namespace Example.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        protected IMemberService MemberService { get; }
        protected IMapper Mapper { get; }

        public MemberController(IMemberService memberService, IMapper mapper)
        {
            MemberService = memberService;
            Mapper = mapper;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(string firstName = null, string lastName = null,
            string favoriteFood = null, float BMI = -1)
        {
            MemberFilter filter = new MemberFilter()
            {
                FirstName = firstName,
                LastName = lastName,
                FavoriteFood = favoriteFood,
                BMI = BMI
            };
            var res = await MemberService.GetAllAsync(filter);
            if (res != null)
            {
                return Ok(res);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var res = await MemberService.GetByIdAsync(id);
            if (res != null)
            {
                return Ok(res);
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Member member)
        {
            var res = await MemberService.AddAsync(member);
            if (res == true)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPost("many")]
        public async Task<IActionResult> PostMany(List<Member> members)
        {
            var res = await MemberService.AddAsync(members);
            if (res == true)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, RestMember restMember)
        {
            var res = await MemberService.UpdateAsync(id, Mapper.Map<Member>(restMember));
            if (res == true)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var res = await MemberService.DeleteAsync(id);
            if (res == true)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}