using CakeShop.DL.Interfaces;
using CakeShop.Models.Models.ModelsSqlDB;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data.SqlClient;

namespace CakeShop.DL.SqlRepositories
{
    public class BakerRepository : IBakerRepository
    {
        private readonly ILogger<BakerRepository> _logger;
        private readonly IConfiguration _configuration;
        public BakerRepository(ILogger<BakerRepository> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        public async Task<Baker> AddBaker(Baker baker)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "INSERT INTO Baker (Name,DateOfBirth,Age,Specialty ) VALUES ( @Name, @DateOfBirth,  @Age,  @Specialty)";
                    var resul = conn.ExecuteAsync(query, baker);
                    return baker;
                }
            }
            catch (Exception)
            {
                _logger.LogError("Could not add baker");
                return null;
            }
        }

        public async Task<Baker?> DeleteBaker(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Baker>("DELETE FROM Baker WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception)
            {
                _logger.LogInformation("Could not delete baker");
                return null;
            }
        }

        public async Task<Baker?> GetBakertById(int id)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Baker>("SELECT * FROM Baker WITH(NOLOCK) WHERE Id = @Id", new { Id = id });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetBakertById)} - {ex.Message}", ex);
            }
            return new Baker();
        }

        public async Task<Baker?> GetBakertByName(string name)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryFirstOrDefaultAsync<Baker>("SELECT * FROM Baker WITH(NOLOCK) WHERE Name = @Name", new { Name = name });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetBakertByName)} - {ex.Message}", ex);
            }
            return new Baker();
        }

        public async Task<IEnumerable<Baker>> GetAllBakers()
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    return await conn.QueryAsync<Baker>("SELECT * FROM Baker");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error in {nameof(GetAllBakers)}:{e.Message}", e);
            }
            return Enumerable.Empty<Baker>();
        }

        public async Task<Baker> UpdateBaker(Baker bakerUpdated)
        {
            try
            {
                await using (var conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await conn.OpenAsync();
                    var query = "UPDATE Baker SET Name = @Name, DateOfBirth = @DateOfBirth, Age = @Age, Specialty = @Specialty WHERE Id = @Id";
                    var result = await conn.ExecuteScalarAsync(query, bakerUpdated);
                    return bakerUpdated;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateBaker)} - {ex.Message}", ex);
            }
            return null;
        }
    }
}
