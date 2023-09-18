using ASP.NET_Core_и_AJAX._Guest_Book.Models;
using ASP.NET_Core_и_AJAX._Guest_Book.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Controllers
{
    public class UserController : Controller
    {
        IRepository _repository;

        public UserController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var users = await _repository.GetUsers();
                if (users.Count == 0)
                { 
                    return Json("Не верный логин или пароль!");
                }
                var users_t = users.Where(a => a.Login == logon.Login);
                if (users_t.ToList().Count == 0)
                { 
                    return Json("Не верный логин или пароль!");
                }
                var user = users_t.First();
                string? salt = user.Salt;

                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);//переводим пароль в байт-массив   
                var md5 = MD5.Create();//создаем объект для получения средств шифрования   
                byte[] byteHash = md5.ComputeHash(password);//вычисляем хеш-представление в байтах 

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                { 
                    return Json("Не верный логин или пароль!"); 
                }
                HttpContext.Session.SetString("Login", user.Login);

                string response = user.Login; 
                return Json(response); 
            }
             
            return Json("Не удалось зайти"); 
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel reg)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Login = reg.Login;

                byte[] saltbuf = new byte[16];

                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);

                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();

                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password); //переводим пароль в байт-массив   
                var md5 = MD5.Create();//создаем объект для получения средств шифрования   
                byte[] byteHash = md5.ComputeHash(password);//вычисляем хеш-представление в байтах

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;

                await _repository.AddUser(user);
                await _repository.Save();
                string response = "Студент успешно добавлен!"; 
                return Json(response); 
            }

            return Json("Не удалось зарегистрироватися!"); 
        }
    }
}
