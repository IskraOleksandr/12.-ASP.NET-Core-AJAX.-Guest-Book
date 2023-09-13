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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel logon)
        {
            if (ModelState.IsValid)
            {
                var users = await _repository.GetUsers();
                if (users.Count == 0)
                {
                    ModelState.AddModelError("", "Не коректный логин или пароль!");
                    return View(logon);
                }
                var users_t = users.Where(a => a.Login == logon.Login);
                if (users_t.ToList().Count == 0)
                {
                    ModelState.AddModelError("", "Не коректный логин или пароль!");
                    return View(logon);
                }
                var user = users_t.First();
                string? salt = user.Salt;

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + logon.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                if (user.Password != hash.ToString())
                {
                    ModelState.AddModelError("", "Не коректный логин или пароль!");
                    return View(logon);
                }
                HttpContext.Session.SetString("Login", user.Login);

                return RedirectToAction("Index", "Message");
            }
            return View(logon);
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

                //переводим пароль в байт-массив  
                byte[] password = Encoding.Unicode.GetBytes(salt + reg.Password);

                //создаем объект для получения средств шифрования  
                var md5 = MD5.Create();

                //вычисляем хеш-представление в байтах  
                byte[] byteHash = md5.ComputeHash(password);

                StringBuilder hash = new StringBuilder(byteHash.Length);
                for (int i = 0; i < byteHash.Length; i++)
                    hash.Append(string.Format("{0:X2}", byteHash[i]));

                user.Password = hash.ToString();
                user.Salt = salt;

                await _repository.AddUser(user);
                await _repository.Save();
         
                return RedirectToAction("Login");
            }

            return View(reg);
        }
    }
}
