using AulersAPI.Models;

namespace AulersAPI.Infrastructure.Interfaces
{
    public interface IPurchasesRepository
    {
        Task<Purchase> GetPurchaseById(int purchaseId);

        Task<List<Purchase>> GetAllUsersPurchases(int userId);

        Task CreatePurchase(Purchase purchase);
    }
}
