using BancoBariConsumer.Models;
using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BancoBariConsumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var result = new List<Message>();

            result = MessageListModel.GetListMessage().OrderByDescending(x => x.DataEnvio).ToList();

            //return result;
            return View(result);
        }

    }
}
