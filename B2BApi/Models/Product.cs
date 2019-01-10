namespace B2BApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public Brand Brand { get; set; }
        public BrandType BrandType { get; set; }
        public string PartNumber { get; set; }
        
    }
}