using CakeShop.Models.Models.ModelsSqlDB;

namespace CakeShop.Models.Models.Requests
{
    public class UpdatePurchaseRequest
    {
        public Guid Id { get; set; }
        public IEnumerable<Cake> Cakes { get; set; }
        public int ClientId { get; set; }
    }
}
