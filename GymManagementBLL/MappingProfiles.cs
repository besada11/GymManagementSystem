using AutoMapper;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GymManagementBLL
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Session, SessionVM>()
                .ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.SessionCategory.CategoryName))
                .ForMember(dest => dest.TrainerName, option => option.MapFrom(src => src.SessionTrainer.Name))
                .ForMember(dest=>dest.AvailableSlots, option => option.Ignore());   

            CreateMap<CreateSessionVM, Session>();

        }
    }
}
