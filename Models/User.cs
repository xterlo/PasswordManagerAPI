namespace PasswordManagerAPI.Models
{
    public class User
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }

    public class UserInfo
    {
        public string Name { get; set; }
        public int Role { get; set; }
    }
}
