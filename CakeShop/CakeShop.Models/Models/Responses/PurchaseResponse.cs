namespace CakeShop.Models.Models.Responses
{
    public class PurchaseResponse
    {
        public Guid Id { get; set; }
        public IEnumerable<Cake> Cakes { get; set; }
        public decimal TotalMoney { get; set; }
        public int ClientId { get; set; }
    }
}
