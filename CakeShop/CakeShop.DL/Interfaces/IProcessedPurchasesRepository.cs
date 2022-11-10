using CakeShop.Models.ModelsMongoDB;
using MongoDB.Bson;

namespace CakeShop.DL.Interfaces
{
    public interface IProcessedPurchasesRepository
    {
        public  Task<Purchase?> AddProcessedPurchase(Purchase purchase);
        public  Task<IEnumerable<Purchase>> GetAllProcessedPurchases();
    }
}
