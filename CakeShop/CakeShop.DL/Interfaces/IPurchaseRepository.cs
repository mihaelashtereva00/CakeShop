using CakeShop.Models.Models.Requests;
using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.DL.Interfaces
{
    public interface IPurchaseRepository
    {
        public Task<IEnumerable<Purchase>> GetPurchases(Guid clientId);
        public  Task<Purchase> GetPurchasesById(Guid purchaseId);
        public Task<Purchase?> CreatePurchase(PurchaseRequest purchase);
        public Task<Purchase> DeletePurchase(Guid purcahseId);
    }
}
