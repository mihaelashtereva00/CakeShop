using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models;

namespace CakeShop.DL.Interfaces
{
    public interface ICakeRepository
    {
        Task<IEnumerable<Cake>> GetAllCakes();
        Task<Cake?> GetCakeById(Guid id);
        Task<Cake> AddCake(CakeRequest cakeRequest);
        Task<Cake> UpdateCake(Cake cake);
        Task<Cake?> DeleteCake(Guid cakeId);
    }
}
