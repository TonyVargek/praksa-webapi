namespace Example.WebApi.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }
        public List<Product> Products { get; set; }
    }
}
