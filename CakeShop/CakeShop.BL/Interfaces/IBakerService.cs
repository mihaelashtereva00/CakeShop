using CakeShop.Models.Models;
using CakeShop.Models.Models.Requests;
using CakeShop.Models.Models.Responses;

namespace CakeShop.BL.Interfaces
{
    public interface IBakerService
    {
        Task<IEnumerable<Baker>> GetAllBakers();
        Task<Baker?> GetBakertById(Guid id);
        Task<BakerResponse> AddBaker(BakerRequest bakerRequest);
        Task<BakerResponse> UpdateBaker(Baker baker);
        Task<Baker?> DeleteBaker(Guid bakerId);
    }
}
