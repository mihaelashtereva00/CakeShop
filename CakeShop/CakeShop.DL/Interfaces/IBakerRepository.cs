using CakeShop.Models.Models;
using CakeShop.Models.Models.Requests;

namespace CakeShop.DL.Interfaces
{
    public interface IBakerRepository
    {
        Task<IEnumerable<Baker>> GetAllBakers();
        Task<Baker?> GetBakertById(Guid id);
        Task<Baker> AddBaker(Baker baker);
        Task<Baker> UpdateBaker(Baker baker);
        Task<Baker?> DeleteBaker(Guid bakerId);
        Task<Baker?> GetBakertByName(string name);
    }
}
