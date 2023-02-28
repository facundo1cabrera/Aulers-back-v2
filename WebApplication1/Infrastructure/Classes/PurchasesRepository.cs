using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Models;
using Dapper;

namespace AulersAPI.Infrastructure.Classes
{
    public class PurchasesRepository : IPurchasesRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public PurchasesRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task CreatePurchase(Purchase purchase)
        {
            using var connection = _connectionFactory.GetConnection();
            var users = await connection.QueryAsync(@"
INSERT INTO [Purchases] 
    ([ClothesId], [UserId], [PurchaseDate]) 
VALUES 
    (@clothesId, @userId, @purchaseDate)
",
            new
            {
                @clothesId = purchase.ClothesId,
                @userId = purchase.UserId,
                @purchaseDate = purchase.PurchaseDate
            });
        }

        public async Task<List<Purchase>> GetAllUsersPurchases(int userId)
        {
            using var connection = _connectionFactory.GetConnection();
            var purchases = (await connection.QueryAsync<Purchase>(@"
SELECT
    [Id],
    [ClothesId],
    [UserId],
    [PurchaseDate]
FROM
    [Purchases]
WHERE
    [UserId]=@userId
",
            new
            {
                @userId = userId
            })).ToList();

            return purchases;
        }

        public async Task<Purchase> GetPurchaseById(int purchaseId)
        {
            using var connection = _connectionFactory.GetConnection();
            var purchase = await connection.QueryFirstOrDefaultAsync<Purchase>(@"
SELECT
    [Id],
    [ClothesId],
    [UserId],
    [PurchaseDate]
FROM
    [Purchases]
WHERE
    [Id]=@purchaseId
",
            new
            {
                @purchaseId = purchaseId
            });

            return purchase;
        }
    }
}
