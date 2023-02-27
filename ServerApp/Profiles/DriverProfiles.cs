using AutoMapper;
using ServerApp.Models;
using ServerApp.Models.DTO.Incoming;
using ServerApp.Models.DTO.Outgoing;

namespace ServerApp.Profiles
{
    public class DriverProfiles : Profile
    {
        public DriverProfiles()
        {
            CreateMap<DriverForCreation, Driver>() // Source, Destination 
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.DriverNumber, opt => opt.MapFrom(src => src.DriverNumber))
                .ForMember(dest => dest.WorldChampionship, opt => opt.MapFrom(src => src.WorldChampionship))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => 1))
                .ForMember(dest => dest.DateAdded, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.DateUpdated, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<Driver, DriverForReturnDto>()
                .ForMember(dest => dest.Fullname, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.WorldChampionship, opt => opt.MapFrom(src => src.WorldChampionship))
                .ForMember(dest => dest.DriverNumber, opt => opt.MapFrom(src => src.DriverNumber));


        }
    }
}
