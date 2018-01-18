using FileDownloader.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace FileDownloader.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            ImageList.InitializeImages();
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View(ImageList.Images);
        }
    }
}
