using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.Models.Models.Responses.PurchaseResponses
{
    public class GetAllPurchaseResponse : BaseResponse
    {
        public IEnumerable<Purchase> Purchases { get; set; }
    }
}
