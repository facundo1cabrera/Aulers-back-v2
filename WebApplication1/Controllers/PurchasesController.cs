using AulersAPI.ApiModels;
using AulersAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AulersAPI.Controllers
{
    [ApiController]
    [Route("api/purchases")]
    public class PurchasesController : ControllerBase
    {
        private readonly IPurchasesService _purchasesService;

        public PurchasesController(IPurchasesService purchasesService)
        {
            _purchasesService = purchasesService;
        }

        [HttpGet("user/{userId:int}")]
        public async Task<ActionResult<List<PurchaseGetDTO>>> GetAllUsersPurchases(int userId)
        {
            var purchases = await _purchasesService.GetAllUsersPurchases(userId);
            
            if (purchases == null)
            {
                return BadRequest();
            }           

            return Ok(purchases);
        }

        [HttpGet("{purchaseId:int}")]
        public async Task<ActionResult<PurchaseGetDTO>> GetPurchaseById(int purchaseId)
        {
            var purchase = await _purchasesService.GetPurchaseById(purchaseId);

            if (purchase == null)
            {
                return BadRequest();
            }
            return Ok(purchase);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePurchase(PurchaseCreationDTO purchase)
        {
            var result = await _purchasesService.CreatePurchase(purchase);

            if (result)
            {
                return Ok();
            }

            return BadRequest("Either the clothesId or the userId did not correspond to a real clothes/user");
        }
    }
}
