namespace NZWalks.Models.DTO
{
    public class RegisterDTO
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string[] Roles { get; set; } = new string[0];
    }
}
