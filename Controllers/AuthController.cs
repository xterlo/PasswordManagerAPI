using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PasswordManagerAPI.Core;
using PasswordManagerAPI.Database;
using PasswordManagerAPI.Models;
using PasswordManagerAPI.Tables;
using System.Text;

namespace PasswordManagerAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class AuthController : ControllerBase
    {
        [HttpPost("Auth")]
        public IActionResult Auth(Auth data)
        {
            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    if (db.user.Where(x => x.Login == data.login).Count() == 0) return BadRequest(ResponseModel.BadRequest("Неверный логин или пароль"));
                    string encryptedPassword = Utils.GetEncodedPassword(data.password, db.user.Where(x => x.Login == data.login).First().Salt);
                    var user = db.user.Where(x => x.Login == data.login && x.Password == encryptedPassword).ToList();
                    if (user.Count == 0) return BadRequest(ResponseModel.BadRequest("Неверный логин или пароль"));
                    else
                    {
                        string SessionToken = Utils.GenerateSessionToken(128);
                        Session session = new Session()
                        {
                            JWT = SessionToken,
                            EndTime = DateTime.Now.AddHours(10),
                            UserId = user.First().Id,
                            //UserAgent = Request.Headers["User-Agent"].ToString() is null ? "Undefind" : Request.Headers["User-Agent"].ToString()
                        };

                        db.session.Add(session);
                        db.SaveChanges();
                        Core.Logger.NewLog($"loggin success: {JsonConvert.SerializeObject(data.login)}", SessionToken);
                        return Ok(ResponseModel.Ok($"Пользователь {user.First().Login} был успешно авторизован!",
                            new AuthResponse()
                            {
                                session = SessionToken,
                                name = db.user.Where(x => x.Id == user.First().Id).Select(x => x.Name).First()
                            }));

                    }
                }
            }
            catch (Exception e)
            {
                return BadRequest(ResponseModel.BadRequest("Ошибка сервера"));
            }
        }
    }
}
