namespace CakeShop.Models.Models.Requests
{
    public class PurchaseRequest
    {
        public IEnumerable<Cake> Cakes { get; set; }
        public decimal TotalMoney { get; set; }
        public int ClientId { get; set; }
    }
}
