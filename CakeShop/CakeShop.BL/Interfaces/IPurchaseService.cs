using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses;
using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.BL.Interfaces
{
    public interface IPurchaseService
    {
        public Task<Purchase> GetPurchases(Guid purchaseId);
        public Task<IEnumerable<Purchase>> GetAllPuechases();
        public Task<PurchaseResponse?> CreatePurchase(PurchaseRequest purchase);
        public Task<Purchase> DeletePurchase(Guid purchaseId);
    }
}
