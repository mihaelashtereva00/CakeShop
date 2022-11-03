using CakeShop.Models.Models.Requests;
using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.DL.Interfaces
{
    public interface IPurchaseRepository
    {
        public Task<Purchase> GetPurchases(int userId);
        public Task<Purchase?> CreatePurchase(PurchaseRequest purchase);
        public Task<Purchase> DeletePurchase(Purchase purchase);
    }
}
