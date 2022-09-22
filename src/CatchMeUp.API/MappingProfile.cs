using AutoMapper;
using CatchMeUp.API.Dto;
using CatchMeUp.Core.Entities;

namespace CatchMeUp.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<MemberInterestDto, UserInterest>().ReverseMap();
        CreateMap<InterestDto, Interest>().ReverseMap();
        CreateMap<FavouriteDto, Favourite>().ReverseMap();
        CreateMap<TeamDto, Team>().ReverseMap();
        CreateMap<TeamEvent, TeamEventDto>().ReverseMap();
        CreateMap<UserInterest, MemberInterestDto>().ReverseMap();
    }
}