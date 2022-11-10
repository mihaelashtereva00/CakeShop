using CakeShop.BL.Kafka;
using CakeShop.DL.Interfaces;
using CakeShop.DL.MongoRepositories;
using CakeShop.DL.SqlRepositories;

namespace CakeShop.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IPurchaseRepository, PurchaseRepository>();
            services.AddSingleton<IProcessedPurchasesRepository, ProcessedPurchaseRepository>();
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<ICakeRepository, CakeRepository>();
            services.AddSingleton<IBakerRepository, BakerRepository>();

            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddHostedService<PurchaseConsumerService>();
            services.AddHostedService<PurcahseProducerService>();

            return services;
        }
    }
}
