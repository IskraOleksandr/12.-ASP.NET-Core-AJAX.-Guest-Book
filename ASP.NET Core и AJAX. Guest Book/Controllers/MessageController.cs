using ASP.NET_Core_и_AJAX._Guest_Book.Models;
using ASP.NET_Core_и_AJAX._Guest_Book.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ASP.NET_Core_и_AJAX._Guest_Book.Controllers
{
    public class MessageController : Controller
    {
        IRepository _repository;

        public MessageController(IRepository repository)
        {
            _repository = repository;
        }

        public ActionResult Index()
        { 
            return View(); 
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        { 
            var tmp = await _repository.GetMessages();
            string response = JsonConvert.SerializeObject(tmp);
            return Json(response);
        }
         
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Json("Ви выйшли"); 
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addMessage(string mes_text)
        {
            var user_login = HttpContext.Session.GetString("Login");

            if (HttpContext.Session.GetString("Login") == null)
                return Json("Сообщение не удалось добавить!");

            var user = await _repository.GetUser(user_login);

            var mes = new Message
            {
                User = user,
                MessageText = mes_text,
                MessageDate = DateTime.Now
            };

            await _repository.AddMessage(mes);
            await _repository.Save(); 
            return Json("Сообщение успешно добавлено!"); 
        }
    }
}