using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.ModelsSqlDB;

namespace CakeShop.DL.Interfaces
{
    public interface ICakeRepository
    {
        Task<IEnumerable<Cake>> GetAllCakes();
        Task<Cake?> GetCakeById(int id);
        Task<Cake> AddCake(Cake cake);
        Task<Cake> UpdateCake(Cake cake);
        Task<Cake?> DeleteCake(int cakeId);
    }
}
