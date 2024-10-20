namespace Store.Data.Entities.Order
{
    public class OrderItems : BaseEntity<Guid>
    {
        public decimal Price { get; set; }

        public int Quantity { get; set; }
        public ProductItem ProductItem {  get; set; } 
        public Guid OrderId { get; set; }
    } 
}
