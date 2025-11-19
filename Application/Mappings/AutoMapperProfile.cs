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
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {

            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();


            CreateMap<Cart, CartDTO>()
                .ForMember(x=>x.PromptTitle, opt=>opt.MapFrom(x=>x.Prompt.Title))
                .ForMember(x=>x.UserName,opt=>opt.MapFrom(x=>x.AppUser.UserName))
                .ReverseMap();


            CreateMap<Favourite, FavouriteDTO>()
                .ForMember(x=>x.PromptTitle,opt=>opt.MapFrom(x=>x.Prompt.Title))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.AppUser.UserName))
                .ReverseMap();


            CreateMap<Prompt, PromptDTO>()
                .ForMember(x=>x.CategoryName,opt=>opt.MapFrom(x=>x.Category.CategoryName))
                .ReverseMap();

            CreateMap<Purchase, PurchaseDTO>()
                .ForMember(x => x.PromptTitle, opt => opt.MapFrom(x => x.Prompt.Title))
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.AppUser.UserName))
                .ReverseMap();

            CreateMap<Subscription, SubscriptionDTO>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.AppUser.UserName))
                .ForMember(x => x.FullName, opt => opt.MapFrom(x => x.AppUser.FullName))
                .ReverseMap();

        }
    }
}
