using Store.Data.Entities;

namespace Store.Service.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string TypeName { get; set; }
        public string BrandName { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
