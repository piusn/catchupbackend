using AutoMapper;
using CatchMeUp.API.Dto;
using CatchMeUp.Core.Entities;

namespace CatchMeUp.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MemberDto, User>().ReverseMap();
        CreateMap<MemberInterestDto, MemberInterest>().ReverseMap();
        CreateMap<InterestDto, Interest>().ReverseMap();
        CreateMap<TeamDto, Team>().ReverseMap();
        CreateMap<TeamEvent, TeamEventDto>().ReverseMap();
        CreateMap<MemberInterest, MemberInterestDto>().ReverseMap();
    }
}