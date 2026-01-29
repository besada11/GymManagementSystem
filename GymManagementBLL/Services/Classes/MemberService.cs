using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService (IGenericRepository<Member> _memberRepository,
        IGenericRepository<MemberShip> _memberShipRepository,
        IPlanRepository _planRepository): IMemberService
    {
        //Add Member
        public bool CreateMember(CreateMemberVM createMemberVM)
        {
            try
            {
                //Check if Phone is exists
                var phoneExists = _memberRepository.GetAll(m => m.Phone == createMemberVM.Phone).Any();
                //Check Email is exists
                var emailExists = _memberRepository.GetAll(m => m.Email == createMemberVM.Email).Any();
                //if one of them exists return false
                if (emailExists || phoneExists) return false;

                //add new member
                var result = _memberRepository.Add(new Member
                {
                    Name = createMemberVM.Name,
                    Email = createMemberVM.Email,
                    Phone = createMemberVM.Phone,
                    Gender = createMemberVM.Gender,
                    DateOfBirth = createMemberVM.DateOfBirth,
                    Address = new Address
                    {
                        BuildingNumber = createMemberVM.BuildingNumber,
                        Street = createMemberVM.Street,
                        City = createMemberVM.City
                    },
                    HealthRecord = new HealthRecord
                    {
                        Weight = createMemberVM.HealthRecordVM.Weight,
                        Height = createMemberVM.HealthRecordVM.Height,
                        BloodType = createMemberVM.HealthRecordVM.BloodType,
                        Note = createMemberVM.HealthRecordVM.Notes
                    }
                });
                return result > 0;
            } 
            catch (Exception)
            {
                return false;
            }
        }

        //Get All Members
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

        //Get MemberDetails By Id
        public MemberVM? GetMemberDetails(int id)
        {
            var member = _memberRepository.GetById(id);
            if (member == null) return null;
            var memberVM = new MemberVM
            {
                Photo = member.Photo,
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}"
            };

            //Active Membership
            var activeMemberShip = _memberShipRepository.GetAll(ms => ms.MemberId == id && ms.Status == "Active").FirstOrDefault();
            if (activeMemberShip != null)
            {
                memberVM.MemberShipStartDate = activeMemberShip.CreatedAt.ToShortDateString();
                memberVM.MemberShipEndDate = activeMemberShip.EndDate.ToShortDateString();
                var plan = _planRepository.GetById(activeMemberShip.PlanId);
                memberVM.PlanName = plan?.Name;
            }
            return memberVM;
        }
    }
}
