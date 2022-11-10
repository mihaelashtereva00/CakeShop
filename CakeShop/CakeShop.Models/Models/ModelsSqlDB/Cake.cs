using MessagePack;

namespace CakeShop.Models.Models.ModelsSqlDB
{
    [MessagePackObject]
    public class Cake
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public int BakerId { get; set; }
        [Key(2)]
        public string Topping { get; set; }
        [Key(3)]
        public string Base { get; set; }
        [Key(4)]
        public string Form { get; set; }
        [Key(5)]
        public decimal Price { get; set; }

    }
}