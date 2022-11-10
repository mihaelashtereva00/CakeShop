using AutoMapper;
using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.AutoMapper
{
    public class AutoMappings : Profile
    {
        public AutoMappings()
        {
            CreateMap<PurchaseRequest, Purchase>();
            CreateMap<Purchase, PurchaseRequest>();
            CreateMap<Purchase, UpdatePurchaseRequest>();
            CreateMap<BakerRequest, Baker>();
            CreateMap<Baker, BakerRequest>();
            CreateMap<Baker, Baker>();
            CreateMap<CakeRequest, Cake>();
            CreateMap<ClientRequest, Client>();
            CreateMap<UpdatePurchaseRequest, Purchase>();
            CreateMap<UpdateBakerRequest, Baker>();
            CreateMap<UpdateCakeRequest, Cake>();
            CreateMap<UpdateClientRequest, Client>();
        }
    }
}
