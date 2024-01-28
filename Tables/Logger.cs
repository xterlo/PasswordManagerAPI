using System.Text.Json.Serialization;

namespace PasswordManagerAPI.Tables
{
    public class Logger
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int UserID { get; set; }
        public int SessionID { get; set; }
        public string Data { get; set; }
        public DateTime Date { get; set; }

        public Users user { get; set; }
        public Session session { get; set; }

    }
}
