using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;

namespace GymManagementBLL.Services.Classes
{
    internal class MemberService (IUnitOfWork _unitOfWork) : IMemberService
    {
        //Create Member
        public bool CreateMember(CreateMemberVM createMemberVM)
        {
            try
            {
                //Check if Phone is exists
                //Check Email is exists
                //if one of them exists return false
                if (IsEmailExists(createMemberVM.Email) || IsPhoneExists(createMemberVM.Phone)) return false;

                //add new member
                 _unitOfWork.GetRepository<Member>().Add(new Member
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
                return _unitOfWork.SaveChanges()>0;
            } 
            catch (Exception)
            {
                return false;
            }
        }

        //Delete Member
        public bool DeleteMember(int memberId)
        {
            var MemberRepo= _unitOfWork.GetRepository<Member>();  
            var MemberShipRepo= _unitOfWork.GetRepository<MemberShip>();

            var member = MemberRepo.GetById(memberId);
            if (member == null) return false;
            var HasActiveMemberSession = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(ms => ms.MemberId == memberId && ms.Session.StartDate > DateTime.Now).Any();
            
            if (HasActiveMemberSession) return false;

            var memberShips =MemberShipRepo .GetAll(ms => ms.MemberId == memberId);
            try
            {
                if(memberShips.Any())
                {
                    foreach (var memberShip in memberShips)
                    {
                        MemberShipRepo.Delete(memberShip);
                    }
                }
                MemberRepo.Delete(member) ;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }

        }

        //Get All Members
        public IEnumerable<MemberVM> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
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

        //Get Member Details By Id
        public MemberVM? GetMemberDetails(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(id);
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
            var activeMemberShip = _unitOfWork.GetRepository<MemberShip>().GetAll(ms => ms.MemberId == id && ms.Status == "Active").FirstOrDefault();
            if (activeMemberShip != null)
            {
                memberVM.MemberShipStartDate = activeMemberShip.CreatedAt.ToShortDateString();
                memberVM.MemberShipEndDate = activeMemberShip.EndDate.ToShortDateString();
                var plan = _unitOfWork.GetRepository<Plan>().GetById(activeMemberShip.PlanId);
                memberVM.PlanName = plan?.Name;
            }
            return memberVM;
        }

        //Get Health Record details
        public HealthRecordVM? GetMemberHealthRecord(int memberId)
        {
           var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberId);
           
            if(memberHealthRecord == null) return null;
            var healthRecordVM = new HealthRecordVM
            {
                Height = memberHealthRecord.Height,
                Weight = memberHealthRecord.Weight,
                BloodType = memberHealthRecord.BloodType,
                Notes = memberHealthRecord.Note
            };
            return healthRecordVM;
        }

        //Get Member To Update
        public MemberToUpdateVM? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if(member == null) return null;
            var memberToUpdateVM = new MemberToUpdateVM
            {
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City
            };
            return memberToUpdateVM;
        }

        //Update Member Details
        public bool UpdateMemberDetails(int memberId, MemberToUpdateVM memberUpdated)
        {
            try
            {
               if (IsEmailExists(memberUpdated.Email) || IsPhoneExists(memberUpdated.Phone)) return false;

                var Member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (Member == null) return false;

                Member.Name = memberUpdated.Name;
                Member.Photo = memberUpdated.Photo;
                Member.Email = memberUpdated.Email;
                Member.Phone = memberUpdated.Phone;
                Member.Address.BuildingNumber = memberUpdated.BuildingNumber;
                Member.Address.Street = memberUpdated.Street;
                Member.Address.City = memberUpdated.City;
                Member.UpdatedAt = DateTime.Now;

                 _unitOfWork.GetRepository<Member>().Update(Member);
                return _unitOfWork.SaveChanges() > 0;
       
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Helper Methods
        private bool IsEmailExists(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == email).Any();
        }

        private bool IsPhoneExists(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(m => m.Phone == phone).Any();
        }
        #endregion
    }
}
