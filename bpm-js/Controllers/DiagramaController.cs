using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bpm_js.Controllers
{
    public class DiagramaController : Controller
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveDiagram(string diagram)
        {


            return Json(new { Status = "Deu Certo" });

        }
    }
}