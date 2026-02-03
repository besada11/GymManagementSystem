using GymManagementBLL.ViewModels.MemberViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    public interface IMemberService
    {
        //Get All Members
        IEnumerable<MemberVM> GetAllMembers();

        //Get MemberDetails By Id
        MemberVM? GetMemberDetails(int id);

        //Create Member
        bool CreateMember(CreateMemberVM createMemberVM);

        //Health Record 
        HealthRecordVM? GetMemberHealthRecord(int memberId);

        //Get Member To Update
        MemberToUpdateVM? GetMemberToUpdate(int memberId);

        //Update Member
        bool UpdateMemberDetails(int memberId, MemberToUpdateVM memberUpdated);

        //Delete Member
        bool DeleteMember(int memberId);
    }
}
