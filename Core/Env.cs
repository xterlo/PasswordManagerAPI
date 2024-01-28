namespace PasswordManagerAPI.Core
{
    public static class Env
    {
        public static string Mode { get; set; } = "Development";
        public static string DBServer { get; set; } = "";
        public static string DBUser { get; set; } = "";
        public static string DBPassword { get; set; } = "";
        public static string DBDatabase { get; set; } = "";


        public static void GrabEnv()
        {
            using (StreamReader reader = new StreamReader("/app/.env"))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    List<string> splitted = line.Split('=').ToList();
                    if (splitted.Count < 2) continue;
                    if (splitted[0] == "Mode") Mode = splitted[1];
                    if (splitted[0] == "DBServer") DBServer = splitted[1];
                    if (splitted[0] == "DBUser") DBUser = splitted[1];
                    if (splitted[0] == "DBPassword") DBPassword = splitted[1];
                    if (splitted[0] == "DBDatabase") DBDatabase = splitted[1];
                }
            }
        }
    }
}
