using CakeShop.Models.Models.ModelsSqlDB;

namespace CakeShop.Models.Models.Requests
{
    public class PurchaseRequest
    {
        public IEnumerable<int> Cakes { get; set; }
        public int ClientId { get; set; }
    }
}
