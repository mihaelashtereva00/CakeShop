using CakeShop.Models.Models;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses;

namespace CakeShop.BL.Interfaces
{
    public interface ICakeService 
    {
        Task<IEnumerable<Cake>> GetAllCakes();
        Task<Cake?> GetCakeById(Guid id);
        Task<CakeResponse> AddCake(CakeRequest cakeRequest);
        Task<CakeResponse> UpdateCake(Cake cake);
        Task<Cake?> DeleteCake(Guid cakeId);
    }
}
