using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {



        public IActionResult Index()
        {
           
            return View(Index);
        }


       

    }
}
