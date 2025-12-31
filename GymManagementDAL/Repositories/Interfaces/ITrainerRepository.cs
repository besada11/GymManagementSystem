using GymManagementDAL.Entities;

namespace GymManagementDAL.Repositories.Interfaces
{
    public interface ITrainerRepository
    {
        //GetAll
        IEnumerable<Trainer> GetAllTrainers();

        //GetById
        Trainer? GetById(int id);

        //Add
        int Add(Trainer trainer);

        //Update
        int Update(Trainer trainer);

        //Delete
        int Delete(Trainer trainer);
    }
}
