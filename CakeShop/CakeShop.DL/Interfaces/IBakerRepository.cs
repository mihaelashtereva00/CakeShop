using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Requests;

namespace CakeShop.DL.Interfaces
{
    public interface IBakerRepository
    {
        Task<IEnumerable<Baker>> GetAllBakers();
        Task<Baker?> GetBakertById(int id);
        Task<Baker> AddBaker(Baker baker);
        Task<Baker> UpdateBaker(Baker baker);
        Task<Baker?> DeleteBaker(int bakerId);
        Task<Baker?> GetBakertByName(string name);
    }
}
