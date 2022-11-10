namespace CakeShop.Models.Models.Requests
{
    public class UpdatePurchseCakesRequest
    {
        public Guid Id { get; set; }
        public IEnumerable<int> Cakes { get; set; }
    }
}
