using Example.Common;
using Example.Model;
using Example.Repository.Common;
using Example.Service.Common;

namespace Example.Service
{
    public class MemberService : IMemberService
    {
        protected IMemberRepository MemberRepository { get; }

        public MemberService(IMemberRepository memberRepository)
        {
            MemberRepository = memberRepository;
        }

        public async Task<bool> AddAsync(Member member)
        {
            return await MemberRepository.AddAsync(member);
        }

        public async Task<bool> AddAsync(List<Member> members)
        {
            return await MemberRepository.AddAsync(members);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await MemberRepository.DeleteAsync(id);
        }

        public async Task<List<Member>> GetAllAsync(MemberFilter filter)
        {
            return await MemberRepository.GetAllAsync(filter);
        }

        public async Task<Member> GetByIdAsync(int id)
        {
            return await MemberRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(int id, Member newMember)
        {
            return await MemberRepository.UpdateAsync(id, newMember);
        }
    }
}