using AutoMapper;
using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.MemberShipsViewModels;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;


namespace GymManagementBLL.Services.Classes
{
    public class MemberShipService(IUnitOfWork _unitOfWork , IMapper _mapper) : IMemberShipService
    {
        //Get all
        public IEnumerable<MemberShipVM> GetAllMemberShips()
        {
            var memberShips = _unitOfWork.MemeberShipReopsitory.GetAllMemberShips(ms=> ms.Status =="Active");
            var memberShipsVM = _mapper.Map<IEnumerable<MemberShipVM>>(memberShips);

            return memberShipsVM;
        }

        //Create membership
        public bool CreateMemberShip(CreateMemberShipVM model)
        {
            if(!MemberExists(model.MemberId)||!PlanExists(model.PlanId)||HasActiveMemberShip(model.MemberId))
                return false;

            var MemberShip = _unitOfWork.MemeberShipReopsitory;
            var memberToCreate = _mapper.Map<MemberShip>(model);
            var paln = _unitOfWork.GetRepository<Plan>().GetById(model.PlanId);
            memberToCreate.EndDate = DateTime.UtcNow.AddDays(paln!.DurationDays);

            MemberShip.Add(memberToCreate);
            return _unitOfWork.SaveChanges() > 0;
        }

        public IEnumerable<MemberForSelectListVM> GetAllMemberForSelectList()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            var membersVM = _mapper.Map<IEnumerable<MemberForSelectListVM>>(members);
            return membersVM;
        }

        public IEnumerable<PlanForSelectListVM> GetAllPlanForSelectList()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll(p => p.IsActive);
            var plansVM = _mapper.Map<IEnumerable<PlanForSelectListVM>>(plans);
            return plansVM;
        }

        public bool DeleteMemberShip(int memberId)
        {
            var membership = _unitOfWork.MemeberShipReopsitory;
            var memberShipToDelete = membership.GetFirstOrDefault(ms => ms.MemberId == memberId && ms.Status == "Active");
            if (memberShipToDelete is null)
                return false;
            membership.Delete(memberShipToDelete);
            return _unitOfWork.SaveChanges() > 0;
        }




        #region Helper Methods
        private bool MemberExists(int memeberId)
        {
            var member =_unitOfWork.GetRepository<Member>().GetById(memeberId);
            return member is not null;
        }
        private bool PlanExists(int PlanId)
        {
            var member = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            return member is not null;
        }
        private bool HasActiveMemberShip(int memeberId)
        {
            var member = _unitOfWork.MemeberShipReopsitory.GetAllMemberShips
                (ms => ms.Status == "Active" && ms.MemberId == memeberId).Any();
            return member;
        }


        #endregion
    }
}
