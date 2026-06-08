namespace Example.WebApi.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Guid StoreId { get; set; }

        public List<Order> Orders { get; set; }
        public Store Store { get; set; }
    }
}
