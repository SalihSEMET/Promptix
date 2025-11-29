using Application.DTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<Prompt, PromptDTO>().ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.PromptCategories.Select(x => x.Category)));

            CreateMap<PromptDTO, Prompt>().ForMember(dest => dest.PromptCategories, opt => opt.Ignore());


            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Favorite, FavoriteDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<PromptCategory, PromptCategoryDTO>().ReverseMap();
            CreateMap<Purchase, PurchaseDTO>().ReverseMap();
            CreateMap<Subscription, SubscriptionDTO>().ReverseMap();



        }
    }
}
