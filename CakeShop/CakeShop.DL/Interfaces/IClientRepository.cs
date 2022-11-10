using CakeShop.Models.Models.ModelsSqlDB;
using CakeShop.Models.Models.Requests;

namespace CakeShop.DL.Interfaces
{
    public interface IClientRepository
    {
        public Task<Client> CreateClient(Client client);
        public Task<Client> Update(Client client);
        public Task<Client> DeleteClient(string username);
        public Task<Client> GetClient(string username);
        public Task<Client> GetClientById(int id);
    }
}
