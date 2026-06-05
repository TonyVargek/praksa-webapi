namespace Example.WebApi
{
    public class Member
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public float BMI => (float)(Weight / ((Convert.ToDouble(Height) / 100) * (Convert.ToDouble(Height) / 100)));

        public int FoodId { get; set; }

        public Food? Food { get; set; }
    }
}
