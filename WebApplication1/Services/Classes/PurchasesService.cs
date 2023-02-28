using AulersAPI.ApiModels;
using AulersAPI.Infrastructure.Interfaces;
using AulersAPI.Models;
using AulersAPI.Services.Interfaces;

namespace AulersAPI.Services.Classes
{
    public class PurchasesService : IPurchasesService
    {
        private readonly IPurchasesRepository _purchasesRepository;
        private readonly IUsersRepository _usersRepository;

        public PurchasesService(IPurchasesRepository purchasesRepository, IUsersRepository usersRepository)
        {
            _purchasesRepository = purchasesRepository;
            _usersRepository = usersRepository;
        }

        public async Task<bool> CreatePurchase(PurchaseCreationDTO purchaseDTO)
        {
            var user = await _usersRepository.GetUserById(purchaseDTO.UserId);

            if (user == null)
            {
                return false;
            }

            // TODO: Validate that clothes exists

            var purchase = new Purchase()
            {
                ClothesId = purchaseDTO.ClothesId,
                UserId = purchaseDTO.UserId,
                PurchaseDate = DateTime.Now
            };

            await _purchasesRepository.CreatePurchase(purchase);
            return true;
        }

        public async Task<List<PurchaseGetDTO>> GetAllUsersPurchases(int userId)
        {
            var user = await _usersRepository.GetUserById(userId);

            if (user == null)
            {
                return null;
            }

            var purchases = await _purchasesRepository.GetAllUsersPurchases(userId);

            var purchasesDTOs = purchases.Select(purchase => new PurchaseGetDTO()
            {
                Id = purchase.Id,
                PurchaseDate = purchase.PurchaseDate,

            }).ToList();

            return purchasesDTOs;
        }

        public async Task<PurchaseGetDTO> GetPurchaseById(int purchaseId)
        {
            var purchase = await _purchasesRepository.GetPurchaseById(purchaseId);

            var purchaseDTO = new PurchaseGetDTO()
            {
                Id = purchase.Id,
                PurchaseDate = purchase.PurchaseDate
            };
            return purchaseDTO;
        }
    }
}
