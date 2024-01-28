using System.Text.Json.Serialization;

namespace PasswordManagerAPI.Tables
{
    public class Session
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string JWT { get; set; }
        public int UserId { get; set; }
        public DateTime EndTime { get; set; }

        public Users User { get; set; }
        public ICollection<Logger> Logger{ get; set; }
    }
}
