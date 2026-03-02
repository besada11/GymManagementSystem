namespace GymManagementBLL.ViewModels.BookingViewModels
{       
    public class MemberForSessionVM
    {
        public string BookingDate { get; set; } = null!;
        public int MemberId { get; set; }
        public bool IsActive { get; set; }
        public string MemberName { get; set; } = null!;
    }
}
