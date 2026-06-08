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
        public bool Add(Member member);
        public bool Add(List<Member> members);
        public bool Update(int id, Member newMember);
        public bool Delete(int id);
        public List<Member> GetAll(MemberFilter filter);
        public Member GetById(int id);
    }
}
