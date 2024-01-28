using System.Text.Json.Serialization;

namespace PasswordManagerAPI.Tables
{
    public class Passwords
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string ServiceName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Key { get; set; }
        public string Description { get; set; } = string.Empty;

        public DateTime creationTime { get; set; }
        public DateTime lastModifiedTime { get; set; }
    }
}
