using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.Models.Models.Responses.PurchaseResponses
{
    public class PurchaseResponse : BaseResponse
    {
        public Purchase Purchase { get; set; }
    }
}
