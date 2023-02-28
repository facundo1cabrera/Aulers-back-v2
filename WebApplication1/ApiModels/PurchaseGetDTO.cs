namespace AulersAPI.ApiModels
{
    public class PurchaseGetDTO
    {
        public int Id { get; set; }
        public decimal Cost { get; set; }
        public string ClothesName { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}
