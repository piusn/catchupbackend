using AutoMapper;
using CatchMeUp.API.Dto;
using CatchMeUp.Core.Entities;

namespace CatchMeUp.API;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MemberDto, Member>().ReverseMap();
        CreateMap<MemberInterestDto, MemberInterest>().ReverseMap();
        CreateMap<InterestDto, Interest>().ReverseMap();
        CreateMap<FavouriteDto, Favourite>().ReverseMap();
    }
}