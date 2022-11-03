using AutoMapper;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.AutoMapper
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<PurchaseRequest, Purchase>();
        }
    }
}
