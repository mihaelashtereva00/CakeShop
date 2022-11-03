using CakeShop.Models.ModelsMongoDB;

namespace CakeShop.Models.Models.Responses
{
    public class PurchaseResponse : BaseResponse
    {
        public Purchase Purchase { get; set; }
    }
}
