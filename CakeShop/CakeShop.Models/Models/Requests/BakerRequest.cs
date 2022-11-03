namespace CakeShop.Models.Models.Requests
{
    public class BakerRequest
    {
        public string Name { get; init; }
        public DateTime DateOfBirth { get; init; }
        public int Age { get; set; }
        public string Specialty { get; set; }
    }
}
