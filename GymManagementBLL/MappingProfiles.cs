using AutoMapper;
using GymManagementBLL.ViewModels.MemberViewModels;
using GymManagementBLL.ViewModels.PlanViewModels;
using GymManagementBLL.ViewModels.SessionViewModels;
using GymManagementBLL.ViewModels.TrainerViewModels;
using GymManagementDAL.Entities;
using Microsoft.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace GymManagementBLL
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            #region Session Mappings

            CreateMap<Session, SessionVM>()
                .ForMember(dest => dest.CategoryName, option => option.MapFrom(src => src.SessionCategory.CategoryName))
                .ForMember(dest => dest.TrainerName, option => option.MapFrom(src => src.SessionTrainer.Name))
                .ForMember(dest=>dest.AvailableSlots, option => option.Ignore());   

            CreateMap<CreateSessionVM, Session>();
            CreateMap<Session , UpdateSessionVM>().ReverseMap();
            CreateMap<Trainer, TrainerSelectVM>();
            CreateMap<Category, CategorySelectVM>()
                .ForMember(dest => dest.Name, option => option.MapFrom(src => src.CategoryName));

            #endregion

            #region Member Mappings

            CreateMap<CreateMemberVM, Member>()
                .ForMember(dest => dest.Address, option => option.MapFrom(src => src))
                .ForMember(dest => dest.HealthRecord, option => option.MapFrom(src => src.HealthRecordVM));


            CreateMap <CreateMemberVM, Address>()
                .ForMember(dest => dest.BuildingNumber, option => option.MapFrom(src => src.BuildingNumber))
                .ForMember(dest => dest.Street, option => option.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, option => option.MapFrom(src => src.City));

            CreateMap<HealthRecordVM, HealthRecord>()
                .ForMember(dest => dest.Note, option => option.MapFrom(src => src.Notes));

            CreateMap<HealthRecord, HealthRecordVM>()
                .ForMember(dest => dest.Notes, option => option.MapFrom(src => src.Note));


            CreateMap<Member, MemberVM>()
                .ForMember(dest=> dest.Gender , option=>option.MapFrom(src=>src.Gender.ToString()))
                .ForMember(dest => dest.Address, option => option.MapFrom(src => $"{src.Address.BuildingNumber} - {src.Address.Street} - {src.Address.City}"))
                .ForMember(dest => dest.DateOfBirth, option => option.MapFrom(src => src.DateOfBirth.ToShortDateString()));


            CreateMap<Member, MemberToUpdateVM>()
                .ForMember(dest => dest.BuildingNumber, option => option.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, option => option.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, option => option.MapFrom(src => src.Address.City));

            CreateMap<MemberToUpdateVM, Member>()
                .ForMember(dest =>dest.Name , option =>option.Ignore())
                .ForMember(dest => dest.Photo, option => option.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });
            #endregion

            #region Trainer Mappings

            CreateMap<CreateTrainerVM, Trainer>()
                .ForMember(dest => dest.Address, option => option.MapFrom(src => new Address
                {
                    Street = src.Street,
                    City = src.City,
                    BuildingNumber = src.BuildingNumber
                }));

            CreateMap<Trainer, TrainerVM>()
                .ForMember(dest => dest.Specialization, option => option.MapFrom(src => src.Specialties.ToString()))
                .ForMember(dest => dest.Address, option => option.MapFrom(src => $"{src.Address.Street}, {src.Address.BuildingNumber}, {src.Address.City}"));

            CreateMap<Trainer, TrainerToUpdateVM>()
                .ForMember(dest => dest.BuildingNumber, option => option.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, option => option.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, option => option.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Specialties, option => option.MapFrom(src => src.Specialties));

            CreateMap<TrainerToUpdateVM, Trainer>()
                .ForMember(dest => dest.Name, option => option.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.Specialties = src.Specialties;
                    dest.UpdatedAt = DateTime.Now;
                });

            #endregion

            #region Plan Mappings

            CreateMap<Plan, PlanVM>();

            CreateMap<Plan, UpdatePlanVM>()
                .ForMember(dest=>dest.PlanName, opt => opt.MapFrom(src => src.Name));

            CreateMap<UpdatePlanVM, Plan>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.UpdatedAt = DateTime.Now;
                });

            #endregion

        }
    }
}
