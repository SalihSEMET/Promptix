using Application.DTO_s;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Prompt, PromptDto>().ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.PromptCategories.Select(x => x.Category)));
        CreateMap<PromptDto, Prompt>().ForMember(dest => dest.PromptCategories, opt => opt.Ignore());
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Favorite, FavoriteDto>().ReverseMap();
        CreateMap<Payment,PaymentDto>().ReverseMap();
        CreateMap<PromptCategory,PromptCategoryDto>().ReverseMap();
        CreateMap<Subscription,SubscriptionDto>().ReverseMap();
        
    }
}