namespace Example.WebApi.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime OrderTime { get; set; }
        public decimal Total { get; set; }
        public Guid UserId { get; set; }
        public Guid DeliveryPersonId { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public DeliveryPerson DeliveryPerson { get; set; }
        public List<Product> Products { get; set; }
        
    }
}
