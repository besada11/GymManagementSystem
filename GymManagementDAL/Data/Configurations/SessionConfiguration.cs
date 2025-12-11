using GymManagementDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GymManagementDAL.Data.Configurations
{
    internal class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(tb =>
            {
                tb.HasCheckConstraint("CK_Session_Capacity", "Capacity BETWEEN 1 AND 25");
                tb.HasCheckConstraint("CK_Session_Dates", "EndDate > StartDate");
            });
        }
    }
}
