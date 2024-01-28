namespace PasswordManagerAPI.Models
{
    public class Auth
    {
        public string login { get; set; }
        public string password { get; set; }
    }

    public class AuthResponse
    {
        public string session { get; set; }
        public string name { get; set; }
    }
}
