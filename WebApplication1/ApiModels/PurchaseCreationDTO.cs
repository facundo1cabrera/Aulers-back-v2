using System.ComponentModel.DataAnnotations;

namespace AulersAPI.ApiModels
{
    public class PurchaseCreationDTO
    {
        [Required]
        public int ClothesId { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
