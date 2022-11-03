using CakeShop.BL.Interfaces;
using CakeShop.BL.Services;
using CakeShop.DL.Interfaces;
using CakeShop.DL.MongoRepositories;
using CakeShop.Models.Models.Responses;

namespace CakeShop.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPurchaseRepository, PurchaseRepository>();
            services.AddSingleton<ICakeRepository, CakeRepository>();
            services.AddSingleton<IBakerRepository, BakerRepository>();

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IPurchaseService, PurchaseService>();
            services.AddSingleton<ICakeService, CakeService>();
            services.AddSingleton<IBakerService, BakerService>();

            return services;
        }
    }
}
