using PasswordManagerAPI.Database;
using PasswordManagerAPI.Models;

namespace PasswordManagerAPI.Core
{
    public static class AuthChecker
    {
        public static ResponseModel IsAuth(string token)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    List<string> allTokens = db.session.Select(x => x.JWT).ToList();
                    if (!allTokens.Contains(token)) return ResponseModel.BadRequest("Неверный токен авторизации");
                    else if (db.session.Where(x => x.JWT == token).First().EndTime < DateTime.Now) return ResponseModel.BadRequest("Истек срок действия сессии, попробуйте авторизоваться заново");
                    else return ResponseModel.Ok("Пользователь авторизован");
                }
            }
            catch (Exception ex)
            {
               return ResponseModel.BadRequest("Ошибка сервера");
            }
        }
    }
}
