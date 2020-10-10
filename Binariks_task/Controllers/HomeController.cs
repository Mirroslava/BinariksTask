using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Binariks_task.Models;
using System.IO;
using System.Text.Json;
using Binariks_task.Services;

namespace Binariks_task.Controllers
{
    public class HomeController : Controller
    {

        HandlerService service = new HandlerService();

        public HomeController()
        {

        }
        public ActionResult Start()
        {
            return View();
        }
        public ActionResult BestAttakingTeam()
        {
            return View(service.GetBestAttakingTeam());
        }
         
        public ActionResult BestProtectiveTeam()
        {
            return View(service.GetBestProtectiveTeam());
        }
        public ActionResult BestScoredMissedTeam()
        {
            return View(service.GetBestScoredMissedTeam());
        }
        public ActionResult MostEffectiveData()
        {
            return View(service.GetMostEffectiveData());
        }
    }
}