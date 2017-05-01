﻿using Exercise.Data;
using Exercise.Models;
using Exercise.Services;
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

        ApplicationDbContext  db = new ApplicationDbContext();

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

            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var currentUser = manager.FindById(User.Identity.GetUserId());

            var userWeight = currentUser.Weight;

            var service = new ExerciseService(userId, userWeight);
            var model = service.GetWorkoutById(id);

            return View(model);
        }
    }
}