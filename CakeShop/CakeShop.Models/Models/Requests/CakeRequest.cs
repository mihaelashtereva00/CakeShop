namespace CakeShop.Models.Models.Requests
{
    public class CakeRequest
    {
        public int BakerId { get; set; }
        public string Topping { get; set; }
        public string Base { get; set; }
        public string Form { get; set; }
        public decimal Price { get; set; }
    }
}
