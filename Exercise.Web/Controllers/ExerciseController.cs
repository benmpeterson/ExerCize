using Exercise.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercise.Web.Controllers
{
    public class ExerciseController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            var model = new ExerciseListItem[0];
            return View(model);
        }
    }
}