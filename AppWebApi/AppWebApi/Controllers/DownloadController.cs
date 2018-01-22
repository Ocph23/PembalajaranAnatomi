using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppWebApi.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Index()
        {
            return View();
        }

        public FileResult Android()
        {
            string rootpath = Server.MapPath("~/AndroidApp/com.Ocph23.Anatomi-Signed.apk");
            byte[] fileBytes = System.IO.File.ReadAllBytes(rootpath);
            string fileName = "Anatomi.apk";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }
    }
}