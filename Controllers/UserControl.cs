using Microsoft.AspNetCore.Mvc;
using PasswordManagerAPI.Core;
using PasswordManagerAPI.Database;
using PasswordManagerAPI.Models;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace PasswordManagerAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]/")]
    public class UserControl : ControllerBase
    {
        [HttpPost("CreateUser")]
        public IActionResult CreateUser(User user)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8); // divide by 8 to convert bits to bytes
            Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");
            string hashed = Utils.GetEncodedPassword(user.Password, Convert.ToBase64String(salt));

            try
            {
                using (ApplicationDBContext db = new ApplicationDBContext())
                {
                    if (db.user.Where(x => x.Login == user.Login).Count() > 0)
                    {

                        return BadRequest(ResponseModel.BadRequest($"Пользователь {user.Login} уже существует"));

                    }

                    db.user.Add(new Tables.Users
                    {

                        Login = user.Login,
                        Password = hashed,
                        Salt = Convert.ToBase64String(salt),
                        Name = user.Name,
                        CreationDate = DateTime.Now,

                    });
                    db.SaveChanges();
                }
                return Ok(ResponseModel.Ok("Пользователь был успешно зарегистрирован"));

            }
            catch (Exception ex)
            {
                return BadRequest(ResponseModel.BadRequest(ex.Message));
            }
        }
        [HttpGet("Me")]
        public IActionResult Me(/*[FromHeader(Name = "SessionToken")]*/ string token)
        {

            using (ApplicationDBContext db = new ApplicationDBContext())
            {
                if (db.session.Select(x => x.JWT).Contains(token))
                {
                    var auth = AuthChecker.IsAuth(token);
                    if (!auth.status) return Ok(auth);
                    var sessionData = db.session.Where(x => x.JWT == token).First();
                    var user = db.user.Where(x => x.Id == sessionData.UserId).First();
                    return Ok(ResponseModel.Ok(data: new UserInfo()
                    {
                        Name = user.Name,
                        Role = user.Id
                    }));
                }
                else return Ok(ResponseModel.BadRequest("Неверный токен авторизаци"));
            }

        }
    }
}
