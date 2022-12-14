namespace CakeShop.Models.Models.Requests
{
    public class UpdateClientRequest
    {
        public int Id { get; set; }
        public string Name { get; init; }
        public DateTime DateOfBirth { get; init; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
