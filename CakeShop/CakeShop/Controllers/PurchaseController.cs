using CakeShop.DL.Interfaces;
using CakeShop.DL.MongoRepositories;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.ModelsMongoDB;
using Microsoft.AspNetCore.Mvc;

namespace CakeShop.Controllers
{
    public class PurchaseController
    {
        private readonly IPurchaseRepository purchaseRepository;

        public PurchaseController(IPurchaseRepository purchaseRepository)
        {
            this.purchaseRepository = purchaseRepository;
        }

        [HttpPost(nameof(AddtoPurchase))]
        public async Task<Purchase> AddtoPurchase(PurchaseRequest purchaseRequest)
        {
            return await purchaseRepository.CreatePurchase(purchaseRequest);
        }
    }
}
