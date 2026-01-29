using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService (IGenericRepository<Member> _memberRepository): IMemberService
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
    }
}
