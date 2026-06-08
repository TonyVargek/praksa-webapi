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
        public bool Add(Member member)
        {
            MemberRepository repository = new MemberRepository();
            return repository.Add(member);
        }

        public bool Add(List<Member> members)
        {
            MemberRepository repository = new MemberRepository();
            return repository.Add(members);
        }

        public bool Delete(int id)
        {
            MemberRepository repository = new MemberRepository();
            return repository.Delete(id);
        }

        public List<Member> GetAll(MemberFilter filter)
        {
            MemberRepository repository = new MemberRepository();
            return repository.GetAll(filter);
        }

        public Member GetById(int id)
        {
            MemberRepository repository = new MemberRepository();
            return repository.GetById(id);
        }

        public bool Update(int id, Member newMember)
        {
            MemberRepository repository = new MemberRepository();
            return repository.Update(id, newMember);
        }
    }
}
