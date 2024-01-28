
using System.Text.Json.Serialization;

namespace PasswordManagerAPI.Tables
{
    public class Users
    {
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public DateTime CreationDate { get; set; }
        

        [JsonIgnore]
        public virtual ICollection<Session>? Session { get; set; }        
        
        [JsonIgnore]
        public virtual ICollection<Logger>? Logger { get; set; }

    }
}
