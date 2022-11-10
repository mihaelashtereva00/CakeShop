namespace CakeShop.Models.Models.Requests
{
    public class PurcahseRequestCakesId
    {
        public IEnumerable<int> Cakes { get; set; }
        public int ClientId { get; set; }
    }
}
