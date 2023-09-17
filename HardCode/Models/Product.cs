namespace HardCode.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ImgProduct { get; set; }
        public string NameProduct { get; set; }
        public string DescriptionProduct { get; set; }
        public int CountProduct { get; set; }
        public int CategoryId { get; set; }
    }
}
