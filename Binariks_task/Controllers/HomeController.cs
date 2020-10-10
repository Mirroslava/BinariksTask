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

        IHandler _service;

        public HomeController()
        {
            _service = new HandlerService();
        }
        public ActionResult Start()
        {
            return View();
        }
        public ActionResult BestAttakingTeam()
        {
            return View(_service.GetBestAttakingTeam());
        }
         
        public ActionResult BestProtectiveTeam()
        {
            return View(_service.GetBestProtectiveTeam());
        }
        public ActionResult BestScoredMissedTeam()
        {
            return View(_service.GetBestScoredMissedTeam());
        }
        public ActionResult MostEffectiveData()
        {
            return View(_service.GetMostEffectiveData());
        }
    }
}