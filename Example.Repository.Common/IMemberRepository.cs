using Example.Common;
using Example.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Repository.Common
{
    public interface IMemberRepository
    {
        public Task<bool> AddAsync(Member member);
        public Task<bool> AddAsync(List<Member> member);
        public Task<bool> UpdateAsync(int id, Member newMember);
        public Task<bool> DeleteAsync(int id);
        public Task<Member> GetByIdAsync(int id);
        public Task<List<Member>> GetAllAsync(MemberFilter filter);
    }
}
