using GymManagementDAL.Entities.Enums;

namespace GymManagementDAL.Entities
{
    internal class Trainer : GymUser
    {
        // HireDate == CreatedAt from BaseClass
        public Specialties Specialties { get; set; }

        public ICollection<Session> TrainerSessions { get; set; } = null!;
    }
}
