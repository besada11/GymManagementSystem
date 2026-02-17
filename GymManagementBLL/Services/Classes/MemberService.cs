using AutoMapper;
using GymManagementBLL.Services.AttachmentService;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using System.Security.Cryptography.Xml;

namespace GymManagementBLL.Services.Classes
{
    public class MemberService (IUnitOfWork _unitOfWork , IMapper _mapper, IAttachmentService _attachmentService) : IMemberService
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

                //Upload Photo
                var photoPath = _attachmentService.UploadFile("members", createMemberVM.PhotoFile);
                if (string.IsNullOrEmpty(photoPath)) return false;

                //add new member
                var memper = _mapper.Map<Member>(createMemberVM);
                memper.Photo = photoPath;
                _unitOfWork.GetRepository<Member>().Add(memper);
                var isCreated = _unitOfWork.SaveChanges() > 0;
                if (!isCreated)
                {
                    _attachmentService.Delete("members", photoPath);
                    return false;
                }
                else
                {
                    return isCreated;
                }
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
            var MemberShipRepo = _unitOfWork.GetRepository<MemberShip>();
            var member = MemberRepo.GetById(memberId);

            if (member == null) return false;

            var SessionId = _unitOfWork.GetRepository<MemberSession>()
                .GetAll(ms => ms.MemberId == memberId).Select(ms => ms.SessionId);

            var HasFutureSessions = _unitOfWork.GetRepository<Session>()
                .GetAll(s => SessionId.Contains(s.Id) && s.StartDate > DateTime.Now).Any();
            if (HasFutureSessions) return false;

            var memberShips =MemberShipRepo.GetAll(ms => ms.MemberId == memberId);
            try
            {
                if(memberShips.Any())
                {
                    foreach (var memberShip in memberShips)
                    {
                        MemberShipRepo.Delete(memberShip);
                    }
                }
                MemberRepo.Delete(member);
                var isDeleted = _unitOfWork.SaveChanges() > 0;
                if (isDeleted)
                    _attachmentService.Delete("members", member.Photo);

                return isDeleted;
                    
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
            if (members == null || !members.Any()) return [];

            var MemberVMs = _mapper.Map<IEnumerable<MemberVM>>(members);
            return MemberVMs;
        }

        //Get Member Details By Id
        public MemberVM? GetMemberDetails(int id)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(id);
            if (member == null) return null;
            var memberVM =_mapper.Map<MemberVM>(member);    

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
            var healthRecordVM =_mapper.Map<HealthRecordVM>(memberHealthRecord);
            return healthRecordVM;
        }

        //Get Member To Update
        public MemberToUpdateVM? GetMemberToUpdate(int memberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if(member == null) return null;
            var memberToUpdateVM = _mapper.Map<MemberToUpdateVM>(member);
            return memberToUpdateVM;
        }

        //Update Member Details
        public bool UpdateMemberDetails(int memberId, MemberToUpdateVM memberUpdated)
        {
            try
            {
                var emailExists = _unitOfWork.GetRepository<Member>().GetAll(m => m.Email == memberUpdated.Email && m.Id != memberId);
                var phoneExists = _unitOfWork.GetRepository<Member>().GetAll(m => m.Phone == memberUpdated.Phone && m.Id != memberId);

                if (emailExists.Any() || phoneExists.Any()) return false;

                var Member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (Member == null) return false;

                // Handle photo update if a new photo is provided
                if (memberUpdated.PhotoFile != null && memberUpdated.PhotoFile.Length > 0)
                {
                    // Delete old photo
                    if (!string.IsNullOrEmpty(Member.Photo))
                    {
                        _attachmentService.Delete("members", Member.Photo);
                    }

                    // Upload new photo
                    var newPhotoPath = _attachmentService.UploadFile("members", memberUpdated.PhotoFile);
                    if (string.IsNullOrEmpty(newPhotoPath))
                    {
                        return false;
                    }
                    Member.Photo = newPhotoPath;
                }

                _mapper.Map(memberUpdated, Member);
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
