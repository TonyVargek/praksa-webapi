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
        public IActionResult GetAll([FromQuery] MemberFilter filter)
        {
            MemberService service = new MemberService();
            var res = service.GetAll(filter);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            MemberService service = new MemberService();
            var res = service.GetById(id);
            if (res != null)
            {
                return Ok(res);
            }
            return BadRequest();
        }

        [HttpPost]
        public IActionResult Post(Member member)
        {
            MemberService service = new MemberService();
            var res = service.Add(member);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPost("many")]
        public IActionResult PostMany(List<Member> members)
        {
            MemberService service = new MemberService();
            var res = service.Add(members);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Member newMember)
        {
            MemberService service = new MemberService();
            var res = service.Update(id, newMember);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            MemberService service = new MemberService();
            var res = service.Delete(id);
            if (res == true)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
