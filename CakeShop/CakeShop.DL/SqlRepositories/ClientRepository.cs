using CakeShop.DL.Interfaces;
using CakeShop.Models.Models.ModelsSqlDB;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace CakeShop.DL.SqlRepositories
{
    public class ClientRepository : IClientRepository
    {

        private readonly ILogger<ClientRepository> _logger;
        private readonly IConfiguration _configuration;
        public ClientRepository(ILogger<ClientRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Client> CreateClient(Client client)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "INSERT INTO Client (Name,DateOfBirth,Username,Password, Email ) VALUES ( @Name, @DateOfBirth,  @Username,  @Password, @Email)";

                    byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); //divided to convert bits to bytes
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                                            password: client.Password!,
                                                            salt: salt,
                                                            prf: KeyDerivationPrf.HMACSHA256,
                                                            iterationCount: 100000,
                                                            numBytesRequested: 256 / 8));
                    client.Password = hashed;
                    var resul = conn.ExecuteAsync(query, client);
                    return client;t;
                }
            }
            catch (Exception)
            {
                _logger.LogError("Could not add client");
                return null;
            }
        }

        public async Task<Client> DeleteClient(string username)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Client>("DELETE FROM Client WHERE Username = @Username", new { Username = username });
                }
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not delete client");
                return null;
            }
        }

        public async Task<Client> GetClient(string username)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Client WITH(NOLOCK) WHERE Username = @Username", new { Username = username });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetClient)} - {ex.Message}", ex);
            }
            return new Client();
        }

        public async Task<Client> GetClientById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Client>("SELECT * FROM Client WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetClient)} - {ex.Message}", ex);
            }
            return new Client();
        }

        public async Task<Client> Update(Client client)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "UPDATE Client SET Name = @Name, DateOfBirth = @DateOfBirth, Username = @Username, Password = @Password, Email = @Email  WHERE Id = @Id";
                    var result = await conn.ExecuteScalarAsync(query, client);
                    return client;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(Update)} - {ex.Message}", ex);
            }
            return null;
        }
    }
}
