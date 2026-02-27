
using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementDAL.Repositories.Classes
{
    public class MemberShipRepository : GenericRepository<MemberShip>, IMemeberShipReopsitory
    {
        private readonly GymDbContext _dbContext;

        public MemberShipRepository(GymDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<MemberShip> GetAllMemberShips(Func<MemberShip, bool>? predicate = null)
        {
            var memberships = _dbContext.MemberShips
                .Include(m => m.Member)
                .Include(m => m.Plan)
                .Where(predicate ?? (_ => true));

            return memberships;
        }

        public MemberShip? GetFirstOrDefault(Func<MemberShip, bool>? predicate = null)
        {
            var membership = _dbContext.MemberShips.FirstOrDefault(predicate ?? (_ => true));   
              
            return membership;
        }
    }
}
