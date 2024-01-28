using Microsoft.EntityFrameworkCore;
using PasswordManagerAPI.Database;

namespace PasswordManagerAPI.Core
{
    public class Logger
    {
        public static void NewLog(string message,string sessionToken)
        {

            using(ApplicationDBContext db = new ApplicationDBContext())
            {
                var session = db.session.Include(x => x.User).Where(x => x.JWT == sessionToken);
                if(sessionToken is null) return;        
                if(session.Count() == 0) return;


                db.Add(new Tables.Logger()
                {
                    SessionID = session.First().Id,
                    Data = message,
                    Date = DateTime.Now,
                    UserID = session.First().UserId,
                });
                db.SaveChanges();
            }
        }
    }
}
