using Exercise.Data;
using Exercise.Models;
using Exercise.Services;
using Exercise.Services.HelperMethods;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercise.Web.Controllers
{
    public class ExerciseController : Controller
    {

        //ApplicationDbContext  db = new ApplicationDbContext();

        [Authorize]
        public ActionResult Index()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));        
            var currentUser = manager.FindById(User.Identity.GetUserId());
            var userWeight = currentUser.Weight;

            var service = new ExerciseService(userId, userWeight);
            var model = service.GetWorkouts();            

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ExerciseCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = Guid.Parse(User.Identity.GetUserId());

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());        
            var userWeight = currentUser.Weight;

            var service = new ExerciseService(userId, userWeight);
            service.CreateExercise(model);

            return RedirectToAction("Index");            
        }

        public ActionResult Details(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ExerciseService(userId);
            var model = service.GetWorkoutById(id);

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            using (var context = new ApplicationDbContext())
            {                

                var query = from b in context.Workouts                            
                            select b.CaloriesBurned;
                List<double> plist = query.ToList();
                ViewBag.intArray = plist;
            }

            

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ExerciseService(userId);
            var model = service.GetWorkoutById(id);

            return View(model);

        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int id)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new ExerciseService(userId);            

            service.DeleteExercise(id);

            TempData["SaveResult"] = "Your exercise was deleted";

            return RedirectToAction("Index");
        }

    }
}