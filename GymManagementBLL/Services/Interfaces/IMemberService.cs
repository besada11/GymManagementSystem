using GymManagementBLL.ViewModels.MemberViewModels;

namespace GymManagementBLL.Services.Interfaces
{
    internal interface IMemberService
    {
        //Get All Members
        IEnumerable<MemberVM> GetAllMembers();
        //Get Member By Id

        //Add Member

        //Update Member

        //Delete Member
    }
}
