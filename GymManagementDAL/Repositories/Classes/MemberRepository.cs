using GymManagementDAL.Data.Context;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementDAL.Repositories.Classes
{
    public class MemberRepository(GymDbContext _dbContext) : IMemberRepository

    {
        //Add
        public int Add(Member member)
        {
            _dbContext.Members.Add(member);
            return _dbContext.SaveChanges();
        }

        //Delete
        public int Delete(Member member)
        {
            var existingMember = _dbContext.Members.Find(member.Id);
            if (existingMember == null) return 0;

            _dbContext.Members.Remove(existingMember);
            return _dbContext.SaveChanges();
        }

        //GetAll
        public IEnumerable<Member> GetAllMembers()
        {
            return _dbContext.Members.ToList();
        }

        //GetById
        public Member? GetById(int id)
        {
            return _dbContext.Members.Find(id);
        }

        //Update
        public int Update(Member member)
        {
            _dbContext.Members.Update(member);
            return _dbContext.SaveChanges();  
        }
    }
}
