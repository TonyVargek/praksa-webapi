using AutoMapper;
using Example.Model;

namespace Example.WebApi;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RestMember, Member>().ReverseMap();
        CreateMap<RestFood, Food>().ReverseMap();
    }
}