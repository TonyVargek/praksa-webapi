namespace Example.WebApi.Models
{
    public class DeliveryPerson
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Vehicle { get; set; }
        public int Age { get; set; }
        public bool Fulltime { get; set; }

        public List<Order> Orders { get; set; }
    }
}
