using Example.Common;
using Example.Model;
using Example.Repository;
using Example.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Service
{
    public class MemberService : IMemberService
    {
        public async Task<bool> AddAsync(Member member)
        {
            MemberRepository repository = new MemberRepository();
            return await repository.AddAsync(member);
        }

        public async Task<bool> AddAsync(List<Member> members)
        {
            MemberRepository repository = new MemberRepository();
            return await repository.AddAsync(members);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            MemberRepository repository = new MemberRepository();
            return await repository.DeleteAsync(id);
        }

        public async Task<List<Member>> GetAllAsync(MemberFilter filter)
        {
            MemberRepository repository = new MemberRepository();
            return await repository.GetAllAsync(filter);
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            MemberRepository repository = new MemberRepository();
            return await repository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Member newMember)
        {
            MemberRepository repository = new MemberRepository();
            return await repository.UpdateAsync(id, newMember);
        }
    }
}
