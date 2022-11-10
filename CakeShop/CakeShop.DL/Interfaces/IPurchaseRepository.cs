using CakeShop.Models.Models.Requests;
using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.DL.Interfaces
{
    public interface IPurchaseRepository
    {
        public Task<IEnumerable<Purchase>> GetPurchasesForClient(int clientId);
        public  Task<Purchase> GetPurchasesById(Guid purchaseId);
        public Task<Purchase?> CreatePurchase(Purchase purchase);
        public Task<Purchase> DeletePurchase(Guid purcahseId);
        public Task<Purchase> UpdatePurchase(Purchase purcahse);
        public Task<IEnumerable<Purchase>> GetPurchasesAfterDate(DateTime date);
    }
}
