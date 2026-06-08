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
    public class MemberController : ControllerBase
    {
        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll([FromQuery] MemberFilter filter)
        {
            MemberService service = new MemberService();
            var res = await service.GetAllAsync(filter);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            MemberService service = new MemberService();
            var res = await service.GetByIdAsync(id);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Post(Member member)
        {
            MemberService service = new MemberService();
            var res = await service.AddAsync(member);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost("many")]
        public async Task<IActionResult> PostMany(List<Member> members)
        {
            MemberService service = new MemberService();
            var res = await service.AddAsync(members);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Member newMember)
        {
            MemberService service = new MemberService();
            var res = await service.UpdateAsync(id, newMember);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            MemberService service = new MemberService();
            var res = await service.DeleteAsync(id);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
