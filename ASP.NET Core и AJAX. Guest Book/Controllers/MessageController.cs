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

        public async Task<ActionResult> Index()
        {
            var model = await _repository.GetMessages();
            return View("Index", model);
        } 

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
         
        [HttpGet]
        public IActionResult addMessage()
        {
            return PartialView("addMessage");
        }

        [HttpPost]
        public async Task<IActionResult> addMessage(string mes_text)
        {
            var user_login = HttpContext.Session.GetString("Login");

            if (HttpContext.Session.GetString("Login") == null)
                return PartialView("~/Views/User/Login.cshtml");

            var user = await _repository.GetUser(user_login);

            var mes = new Message
            {
                User = user,
                MessageText = mes_text,
                MessageDate = DateTime.Now
            };

            await _repository.AddMessage(mes);
            await _repository.Save();
            return PartialView("~/Views/Message/Success.cshtml");
        }
    }
}