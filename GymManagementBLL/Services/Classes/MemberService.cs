using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService (IGenericRepository<Member> _memberRepository): IMemberService
    {
        public IEnumerable<MemberVM> GetAllMembers()
        {
            var members = _memberRepository.GetAll();
            if (members == null || members.Any()) return [];

            var MemberVMs = members.Select(m => new MemberVM
            {
                Id = m.Id,
                Photo = m.Photo,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Gender = m.Gender.ToString()
            });
            return MemberVMs;
        }
    }
}
