using GymManagementBLL.ViewModels.MemberViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IMemberService
    {
        //Get All Members
        IEnumerable<MemberVM> GetAllMembers();
        //Get MemberDetails By Id
        MemberVM? GetMemberDetails(int id);
        //Create Member
        bool CreateMember(CreateMemberVM createMemberVM);
        //Health Record 
        HealthRecordVM? GetMemberHealthRecord(int memberId);

        //Delete Member
    }
}
