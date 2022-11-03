using CakeShop.Models.Models;

namespace CakeShop.Models.ModelsMongoDB
{
    public class Purchase
    {
        public Guid Id { get; init; }
        public IEnumerable<Cake> Cakes { get; set; }
        public decimal TotalMoney { get; set; }
        public int ClientId { get; set; }
    }
}
