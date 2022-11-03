namespace CakeShop.Models.Models
{
    public class Cake
    {
        public Guid Id { get; set; }
        public Guid BakerId { get; set; }
        public string Topping { get; set; }
        public string Base { get; set; }
        public string Form { get; set; }
        public decimal Price { get; set; }

    }
}