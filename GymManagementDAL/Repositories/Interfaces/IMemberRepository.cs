using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        //GetAll
        IEnumerable<Member> GetAllMembers();

        //GetById
        Member? GetById(int id);    

        //Add
        int Add (Member member);

        //Update
        int Update(Member member);

        //Delete
        int Delete(Member member);
    }
}
