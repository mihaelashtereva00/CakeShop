using CakeShop.Models.Interfaces;
using CakeShop.Models.Models.ModelsSqlDB;
using MessagePack;
using KeyAttribute = MessagePack.KeyAttribute;

namespace CakeShop.Models.ModelsMongoDB
{
    [MessagePackObject]
    public class Purchase : ICacheItem<Guid>
    {
        [Key(0)]
        public Guid Id { get; set; }
        [Key(1)]
        public IEnumerable<Cake> Cakes { get; set; }
        [Key(2)]
        public decimal TotalMoney { get; set; }
        [Key(3)]
        public int ClientId { get; set; }
        [Key(4)]
        public DateTime Date { get; set; }

        public Guid GetKey() => Id; 
    }
}
