using Example.Common;
using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service.Common
{
    public interface IMemberService
    {
        public Task<bool> AddAsync(Member member);
        public Task<bool> AddAsync(List<Member> members);
        public Task<bool> UpdateAsync(int id, Member newMember);
        public Task<bool> DeleteAsync(int id);
        public Task<List<Member>> GetAllAsync(MemberFilter filter);
        public Task<Member> GetByIdAsync(int id);
    }
}
