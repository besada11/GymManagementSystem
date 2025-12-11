using System.Reflection.PortableExecutable;

namespace GymManagementDAL.Entities
{
    internal class Session : BaseClass
    {
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        //Relationship Category - Session (Many to One)
        public Category SessionCategory { get; set; } = null!;
        public int CategoryId { get; set; }


        //Relationship Trainer - Session (Many to One)
        public Trainer SessionTrainer { get; set; } = null!;
        public int TrainerId { get; set; }


        //Relationship Session - MemberSession (One to Many)
        public ICollection<MemberSession> SessionMembers { get; set; } = null!;




    }
}
