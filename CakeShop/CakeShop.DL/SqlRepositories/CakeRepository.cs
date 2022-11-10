using CakeShop.DL.Interfaces;
using CakeShop.Models.Models.ModelsSqlDB;
using Dapper;
using DnsClient.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace CakeShop.DL.SqlRepositories
{
    public class CakeRepository : ICakeRepository
    {
        private readonly ILogger<CakeRepository> _logger;
        private readonly IConfiguration _configuration;
        public CakeRepository(ILogger<CakeRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<Cake> AddCake(Cake cake)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "INSERT INTO Cake (BakerId,Topping,Base,Form,Price) VALUES ( @BakerId, @Topping,  @Base,  @Form, @Price)";
                    var resul = conn.ExecuteAsync(query, cake);
                    return cake;
                }
            }
            catch (Exception)
            {
                _logger.LogError("Could not add cake");
                return null;
            }
        }

        public async Task<Cake?> DeleteCake(int cakeId)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Cake>("DELETE FROM Cake WHERE Id = @Id", new { Id = cakeId });
                }
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not delete cake");
                return null;
            }
        }

        public async Task<IEnumerable<Cake>> GetAllCakes()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Cake>("SELECT * FROM Cake");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllCakes)}:{e.Message}", e);
            }
            return Enumerable.Empty<Cake>();
        }

        public async Task<Cake?> GetCakeById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Cake>("SELECT * FROM Cake WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCakeById)} - {ex.Message}", ex);
            }
            return new Cake();
        }
        public async Task<Cake> UpdateCake(Cake cake)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "UPDATE Cake SET BakerId = @BakerId, Topping = @Topping, Base = @Base, Form = @Form, Price = @Price  WHERE Id = @Id";
                    var result = await conn.ExecuteScalarAsync(query, cake);
                    return cake;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateCake)} - {ex.Message}", ex);
            }
            return null;
        }
    }

}



