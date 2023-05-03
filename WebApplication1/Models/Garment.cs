namespace AulersApi.Models
{
    public class Garment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PrimaryUrl { get; set; }
        public string ModelUrl { get; set; }
        public char Gender { get; set; }
        public string Size { get; set; }
        public bool IsDeleted { get; set; }
        public decimal Price { get; set; }
        public DateTime SinceDate { get; set; }
        public bool Available { get; set; }

    }
}
