namespace AulersAPI.Models
{
    public class Purchase
    {
        public int Id { get; set; }
        public int ClothesId { get; set; }
        public int UserId { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
