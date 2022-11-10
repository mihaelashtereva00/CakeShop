namespace CakeShop.Models.Interfaces
{
    public interface ICacheItem<out T>
    {
        T GetKey();

    }
}
