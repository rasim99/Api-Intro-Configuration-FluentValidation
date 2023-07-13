using AutoMapper;
using FirstApiProject.Dtos.Category;
using FirstApiProject.Dtos.Product;
using FirstApiProject.Dtos.User;
using FirstApiProject.Extension;
using FirstApiProject.Models;

namespace FirstApiProject.Profiles
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryReturnDto>()
                .ForMember(ds => ds.ImageUrl, map => map.MapFrom(sr => "https://localhost:7278/" +sr.ImageUrl));
            
            CreateMap<Category, CategoryInProductReturnDto>();
            CreateMap<Product, ProductReturnDto>()
                .ForMember(ds => ds.Profit, map => map.MapFrom(s => s.SalePrice - s.CostPrice));

            
        }
    }
}
