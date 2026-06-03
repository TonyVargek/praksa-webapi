namespace Example.WebApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public List<Order> Orders { get; set; }
    }
}
