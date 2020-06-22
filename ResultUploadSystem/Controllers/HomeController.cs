using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResultUploadSystem.Dao.IDao;
using ResultUploadSystem.Models;

namespace ResultUploadSystem.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            List<object> list = new List<object>();
            for (int i = 0; i < 50; i++)
            {
                list.Add(new { 
                    id=i+1,
                    Username="张三"+i
                });
            }
            
            return Json(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
