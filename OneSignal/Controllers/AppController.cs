using OneSignal.Models;
using OneSignal.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneSignal.Controllers
{
    public class AppController : Controller
    {
        private IAppRepository _APIRepository;

        public AppController()
        {
            _APIRepository = new AppRepository();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult CreateApp()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateApp(App app)
        {
            if (ModelState.IsValid)
            {
                if (_APIRepository.CreateApp(app))
                    ViewBag.CreationMessage = "App was created successfully";
                else
                    ViewBag.CreationMessage = "App failed to be created";

                return View();
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateApp(App app)
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [ActionName("UpdateApp")]
        [HttpPost]
        public ActionResult UpdateApp_Post(App app)
        {
            if (ModelState.IsValid)
            {
                if (_APIRepository.UpdateApp(app))
                    ViewBag.UpdateMessage = "App was updated successfully";
                else
                    ViewBag.UpdateMessage = "App failed to be updated";

                return View();
            }
            return View();
        }

        public ActionResult ViewApp(App app)
        {
            return View(app);
        }

        public ActionResult ViewAllApps()
        {
            var listOfApps = _APIRepository.ViewAllApps();
            return View("AppsInfo", listOfApps);
        }
    }
}