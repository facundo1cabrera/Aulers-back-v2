using AulersAPI.ApiModels;
using AulersAPI.Models;

namespace AulersAPI.Services.Interfaces
{
    public interface IPurchasesService
    {
        Task<PurchaseGetDTO> GetPurchaseById(int purchaseId);

        Task<List<PurchaseGetDTO>> GetAllUsersPurchases(int userId);

        Task<bool> CreatePurchase(PurchaseCreationDTO purchaseCreationDTO);
    }
}
