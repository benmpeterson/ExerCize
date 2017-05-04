using Exercise.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercise.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult CustomerData()
        {
            using (var context = new ApplicationDbContext())
            {
                var blogs = context.Database.SqlQuery<string>("SELECT Id FROM dbo.ApplicationUser").ToList();
                var TotalCalories = context.Database.SqlQuery<double>("SELECT CaloriesBurned FROM dbo.Workout").ToList();
                var TotalCaloriesSum = TotalCalories.Sum();
                var ListTypeOfWorkouts = context.Database.SqlQuery<string>("SELECT Type FROM dbo.Workout").ToList();
                double walkCount = 0;
                double runCount = 0;
                double bikeCount = 0;
                double danceCount = 0;
                double swimCount = 0;

                foreach (var item in ListTypeOfWorkouts)
                {
                    if (item == "Walking")
                    {
                        walkCount = walkCount + 1;
                    }
                    else if (item == "Running")
                    {
                        runCount = runCount + 1;
                    }
                    else if (item == "Bicycling")
                    {
                        bikeCount = danceCount + 1;
                    }
                    else if (item == "Dancing")
                    {
                        danceCount = danceCount + 1;
                    }
                    else if (item == "Swimming")
                    {
                        swimCount = swimCount + 1;
                    }
                }

                ViewBag.WalkStat = walkCount;
                ViewBag.RunStat = runCount;
                ViewBag.BikeStat = bikeCount;
                ViewBag.DanceStat = danceCount;
                ViewBag.SwimStat = swimCount;

            }

             


            return View();
        }
    }
}